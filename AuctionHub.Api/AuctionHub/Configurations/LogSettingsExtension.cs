using Serilog;

namespace AuctionHub.Configurations
{
    public static class LogSettingsExtension
    {
        public static void AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.File("C:/temp/AuctionHubLog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });
        }
    }
}
