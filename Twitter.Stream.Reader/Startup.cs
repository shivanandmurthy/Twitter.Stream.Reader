using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Twitter.Stream.Reader.Core;
using Twitter.Stream.Reader.Infrastructure;
using Twitter.Stream.Reader.Infrastructure.model;

namespace Twitter.Stream.Reader
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
            HostEnvironment = hostEnvironment;
        }

        public void Configure()
        {
            //Nothing to configure at this time            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClient();
            services.Configure<FeedConfigSettings>(Configuration);
            services.AddTransient<IAnalyzeService, AnalyzeService>();
            services.AddTransient<IDbInitializer, DbInitializer>();
            _logger.LogInformation("Di complete");
        }
    }
}
