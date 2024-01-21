using AuctionHub.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace AuctionHub.Infrastructure
{
    public class AuctionHubDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public DbSet<BiddingRoom> BiddingRooms { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public AuctionHubDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here
            modelBuilder.Entity<BiddingRoom>()
                .HasMany(room => room.Bids)
                .WithOne(bid => bid.BiddingRoom)
                .HasForeignKey(bid => bid.BiddingRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invoice>()
                .HasOne(invoice => invoice.BiddingRoom)
                .WithMany(room => room.Bids)
                .HasForeignKey(invoice => invoice.BiddingRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
