using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using Twitter.Stream.Reader;
using Twitter.Stream.Reader.Infrastructure;

namespace Twitter.Stream.Reader.Core
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var pathToContentRoot = Directory.GetCurrentDirectory();

			IConfigurationRoot configuration = new ConfigurationBuilder()
			  .SetBasePath(pathToContentRoot)
			  .AddJsonFile("appsettings.json")
			  .Build();

			Serilog.Debugging.SelfLog.Enable(Console.Error);

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
			try
			{
				IWebHost host = CreateWebHostBuilder(args).Build();
				using IServiceScope scope = host.Services.CreateScope();
				IServiceProvider services = scope.ServiceProvider;

				try
				{
					// in future: hooks for ef migrations.
					var initializer = services.GetRequiredService<IDbInitializer>();
					initializer.EnsureMigration();
					initializer.Initialize();
				}
				catch (Exception ex)
				{
					Log.Error(ex, "An error occurred while seeding the twitter stream reader database.");
					throw;
				}
				var analyzeService = services.GetRequiredService<IAnalyzeService>();
				await analyzeService.ReadStreamData(); 

			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred analyzing the twitter stream. Check logs for additional error messages.");
				throw;
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseSerilog();
		}
	}
}
