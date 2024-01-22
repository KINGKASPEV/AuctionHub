﻿namespace AuctionHub.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IBiddingRoomRepository BiddingRooms { get; }
        IBidRepository Bids { get; }
        IInvoiceRepository Invoices { get; }
        IPaymentRepository Payments { get; }
        INotificationRepository Notifications { get; }
        void SaveChanges();
        void Dispose();
    }
}
