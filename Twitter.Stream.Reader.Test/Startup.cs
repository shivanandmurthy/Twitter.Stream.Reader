using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twitter.Stream.Reader.Core;

namespace Twitter.Stream.Reader.Test
{
	public class Startup
    {
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<IAnalyzeService, AnalyzeServiceTest>();
        }
    }
}
