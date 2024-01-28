using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Domain.Entities;
using AuctionHub.Domain;
using Microsoft.Extensions.Logging;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Application.DTOs.Notifications;
using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Application.DTOs.BiddingRoom;

namespace AuctionHub.Application.ServiceImplementations
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IUnitOfWork unitOfWork, ILogger<NotificationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<NotificationResponseDto>> NotifyParticipantsAsync(BidRequestDto BidRequestDto)
        {
            try
            {
                // Business logic to notify participants of a new bid
                var notification = new Notification
                {
                    Message = $"New bid of {BidRequestDto.Amount} submitted.",
                    NotificationTime = DateTime.UtcNow
                };

                await _unitOfWork.Notifications.CreateNotificationAsync(notification);
                _unitOfWork.SaveChanges();

                // Populate NotificationResponseDto with relevant data
                var notificationResponseDto = new NotificationResponseDto
                {
                    Message = notification.Message,
                    NotificationTime = notification.NotificationTime
                };

                return ApiResponse<NotificationResponseDto>.Success(notificationResponseDto, "Notification sent successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while notifying participants.");
                return ApiResponse<NotificationResponseDto>.Failed(false, "Error occurred while notifying participants.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<NotificationResponseDto>> NotifyAuctionConclusionAsync(BiddingRoomRequestDto BiddingRoomRequestDto)
        {
            try
            {
                // Business logic to notify participants of auction conclusion
                var notification = new Notification
                {
                    Message = $"Auction for item '{BiddingRoomRequestDto.ItemName}' has concluded. Winner: {BiddingRoomRequestDto.WinningBid.Amount}.",
                    NotificationTime = DateTime.UtcNow
                };

                await _unitOfWork.Notifications.CreateNotificationAsync(notification);
                _unitOfWork.SaveChanges();

                // Populate NotificationResponseDto with relevant data
                var notificationResponseDto = new NotificationResponseDto
                {
                    Message = notification.Message,
                    NotificationTime = notification.NotificationTime
                };

                return ApiResponse<NotificationResponseDto>.Success(notificationResponseDto, "Notification sent successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while notifying auction conclusion.");
                return ApiResponse<NotificationResponseDto>.Failed(false, "Error occurred while notifying auction conclusion.", 500, new List<string> { ex.Message });
            }
        }
    }
}
