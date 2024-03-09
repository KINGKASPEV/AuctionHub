using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using AuctionHub.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AuctionHub.Application.ServiceImplementations
{
    public class BiddingRoomService : IBiddingRoomService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BiddingRoomService> _logger;
        private readonly IModel _channel;

        public BiddingRoomService(IUnitOfWork unitOfWork, ILogger<BiddingRoomService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        //public BiddingRoomService(IUnitOfWork unitOfWork, ILogger<BiddingRoomService> logger, RabbitMQConfig rabbitMQConfig)
        //{
        //    _unitOfWork = unitOfWork;
        //    _logger = logger;

        //    if (rabbitMQConfig == null)
        //    {
        //        _logger.LogError("RabbitMQConfig cannot be null.");
        //        throw new ArgumentNullException(nameof(rabbitMQConfig));
        //    }

        //    try
        //    {
        //        _logger.LogInformation($"Connecting to RabbitMQ with HostName: {rabbitMQConfig.HostName}, Port: {rabbitMQConfig.Port}");

        //        var connectionfactory = new ConnectionFactory
        //        {
        //            HostName = rabbitMQConfig.HostName,
        //            UserName = rabbitMQConfig.UserName,
        //            Password = rabbitMQConfig.Password,
        //            Port = rabbitMQConfig.Port,
        //            VirtualHost = rabbitMQConfig.VirtualHost,
        //        };

        //        var connection = connectionfactory.CreateConnection();
        //        _channel = connection.CreateModel();

        //        _logger.LogInformation("Connected to RabbitMQ.");

        //        _channel.QueueDeclare(queue: "bidding.start", durable: false, exclusive: false, autoDelete: false, arguments: null);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error establishing RabbitMQ connection.");
        //        throw;
        //    }
        //}

        public async Task<ApiResponse<string>> StartAuctionAsync(BiddingRoomRequestDto biddingRoomRequestDto)
        {
            try
            {
                var biddingRoom = new BiddingRoom
                {
                    RoomName = biddingRoomRequestDto.RoomName,
                    IsAuctionActive = biddingRoomRequestDto.IsAuctionActive,
                    EndTime = biddingRoomRequestDto.EndTime,
                    ItemName = biddingRoomRequestDto.ItemName
                };

                if (!biddingRoom.IsAuctionActive)
                {
                    biddingRoom.IsAuctionActive = true;
                    biddingRoom.EndTime = DateTime.UtcNow.AddHours(1);

                    await _unitOfWork.BiddingRooms.UpdateBiddingRoomAsync(biddingRoom);
                     _unitOfWork.SaveChanges();

                    //PublishStartAuctionMessage(biddingRoomRequestDto.ItemName);

                    return ApiResponse<string>.Success("Auction started successfully.", "Auction started", 200);
                }
                else
                {
                    return ApiResponse<string>.Failed(false, "Auction is already active.", 400, new List<string> { "Auction is already active." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while starting the auction.");
                return ApiResponse<string>.Failed(false, "Error occurred while starting the auction.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<BiddingRoomResponseDto>> CreateBiddingRoomAsync(BiddingRoomRequestDto biddingRoomRequestDto)
        {
            try
            {
                var biddingRoom = new BiddingRoom
                {
                    RoomName = biddingRoomRequestDto.RoomName,
                    IsAuctionActive = biddingRoomRequestDto.IsAuctionActive,
                    EndTime = biddingRoomRequestDto.EndTime,
                    ItemName = biddingRoomRequestDto.ItemName
                };

                await _unitOfWork.BiddingRooms.AddAsync(biddingRoom);
                _unitOfWork.SaveChanges();

                var biddingRoomResponseDto = new BiddingRoomResponseDto
                {
                    BiddingRoomId = biddingRoom.Id.ToString(), 
                    RoomName = biddingRoom.RoomName,
                    IsAuctionActive = biddingRoom.IsAuctionActive,
                    EndTime = biddingRoom.EndTime,
                    ItemName = biddingRoom.ItemName
                };

                var successResponse = ApiResponse<BiddingRoomResponseDto>.Success(
                    data: biddingRoomResponseDto,
                    message: "Bidding room created successfully.",
                    statusCode: 201);

                return successResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a bidding room.");
                return ApiResponse<BiddingRoomResponseDto>.Failed(
                    errors: new List<string> { ex.Message });
            }
        }



        public async Task<ApiResponse<List<BiddingRoomResponseDto>>> GetAllBiddingRoomsAsync()
        {
            try
            {
                var biddingRooms = await _unitOfWork.BiddingRooms.GetAllAsync();

                var responseDtoList = biddingRooms.Select(br => new BiddingRoomResponseDto
                {
                    BiddingRoomId = br.Id,
                    RoomName = br.RoomName,
                    IsAuctionActive = br.IsAuctionActive,
                    EndTime = br.EndTime,
                    ItemName = br.ItemName
                }).ToList();

                return ApiResponse<List<BiddingRoomResponseDto>>.Success(responseDtoList, "Bidding rooms retrieved successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all bidding rooms.");
                return ApiResponse<List<BiddingRoomResponseDto>>.Failed(false, "Error occurred while getting all bidding rooms.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<bool>> DeleteBiddingRoomByIdAsync(string biddingRoomId)
        {
            try
            {
                var biddingRoom = await _unitOfWork.BiddingRooms.GetByIdAsync(biddingRoomId);

                if (biddingRoom == null)
                {
                    return ApiResponse<bool>.Failed(false, "Bidding room not found.", 404, new List<string> { "Bidding room not found." });
                }

                _unitOfWork.BiddingRooms.Delete(biddingRoom);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "Bidding room deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a bidding room.");
                return ApiResponse<bool>.Failed(false, "Error occurred while deleting a bidding room.", 500, new List<string> { ex.Message });
            }
        }

        private void PublishStartAuctionMessage(string biddingRoomId)
        {
            var biddingRoomMessage = new
            {
                BiddingRoomId = biddingRoomId,

                Action = "StartAuction"
            };

            var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(biddingRoomMessage));

            _channel.BasicPublish(exchange: "", routingKey: "bidding.start", basicProperties: null, body: message);
        }
        public void Dispose()
        {
            _channel?.Dispose();
        }
    }
}

