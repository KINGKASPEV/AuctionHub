using AuctionHub.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.Infrastructure
{
    public class AuctionHubDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<BiddingRoom> BiddingRooms { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Notification> Notifications { get; set; } 


        public AuctionHubDbContext(DbContextOptions<AuctionHubDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BiddingRoom>()
                .HasMany(room => room.Bids)
                .WithOne(bid => bid.BiddingRoom)
                .HasForeignKey(bid => bid.BiddingRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BiddingRoom>()
                .HasOne(room => room.WinningBid)
                .WithOne()
                .HasForeignKey<BiddingRoom>(room => room.WinningBidId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Invoice>()
                .HasOne(invoice => invoice.WinningBid)
                .WithOne()
                .HasForeignKey<Invoice>(invoice => invoice.WinningBidId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Payment>()
                 .HasOne(payment => payment.Invoice)
                 .WithMany(invoice => invoice.Payments) 
                 .HasForeignKey(payment => payment.InvoiceId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .Property(n => n.NotificationTime)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
