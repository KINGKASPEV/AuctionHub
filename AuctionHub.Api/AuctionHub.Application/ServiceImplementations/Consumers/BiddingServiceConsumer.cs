using AuctionHub.Domain.Entities;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace AuctionHub.Application.ServiceImplementations.Consumers
{
    public class BiddingServiceConsumer
    {
        private readonly IModel _channel;

        public BiddingServiceConsumer(RabbitMQConfig rabbitMQConfig)
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password,
                Port = rabbitMQConfig.Port,
                VirtualHost = rabbitMQConfig.VirtualHost,
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void StartConsuming()
        {
            try
            {
                _channel.QueueDeclare(queue: "bidding.start", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var biddingRoomId = Encoding.UTF8.GetString(body);

                        StartAuction(biddingRoomId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error handling bidding.start message: {ex.Message}");
                    }
                };

                _channel.BasicConsume(queue: "bidding.start", autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up BiddingServiceConsumer: {ex.Message}");
            }
            finally
            {
                _channel.Dispose();
                _channel.Close();
            }
        }

        private void StartAuction(string biddingRoomId)
        {
            Console.WriteLine($"Auction started for bidding room: {biddingRoomId}");
        }
    }

}
