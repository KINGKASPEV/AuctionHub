namespace AuctionHub.Domain.Entities
{
    public class RabbitMQConfig
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }

        //public virtual IConnectionFactory CreateConnectionFactory()
        //{
        //    var connectionFactory = new ConnectionFactory
        //    {
        //        HostName = this.HostName,
        //        UserName = this.UserName,
        //        Password = this.Password,
        //        Port = this.Port,
        //        VirtualHost = this.VirtualHost,
        //        AutomaticRecoveryEnabled = true
        //    };
        //    return connectionFactory;
        //}
    }
}
