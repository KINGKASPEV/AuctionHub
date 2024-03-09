using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Application.DTOs.Notifications;
using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using AuctionHub.Domain.Entities;
using Microsoft.Extensions.Logging;

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

        public async Task<ApiResponse<NotificationResponseDto>> NotifyParticipantsAsync(BidRequestDto bidRequestDto)
        {
            try
            {
                var notification = new Notification
                {
                    Message = $"New bid of {bidRequestDto.Amount} submitted.",
                    NotificationTime = DateTime.UtcNow
                };

                await _unitOfWork.Notifications.CreateNotificationAsync(notification);
                _unitOfWork.SaveChanges();

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

        public async Task<ApiResponse<NotificationResponseDto>> NotifyAuctionConclusionAsync(BiddingRoomRequest biddingRoomRequest)
        {
            try
            {
                var notification = new Notification
                {
                    Message = $"Auction for item '{biddingRoomRequest.ItemName}' has concluded. Winner: {biddingRoomRequest.WinningBid.Amount}.",
                    NotificationTime = DateTime.UtcNow
                };

                await _unitOfWork.Notifications.CreateNotificationAsync(notification);
                _unitOfWork.SaveChanges();

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
