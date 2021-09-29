using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twitter.Stream.Reader.Infrastructure.model;

namespace Twitter.Stream.Reader.Core
{
    public class AnalyzeService : IAnalyzeService
    {
        private const int ONE_MINUTE = 60;
        private readonly IHttpClientFactory _clientFactory;
        private readonly FeedConfigSettings _feedConfigSettings;
        private readonly ILogger<AnalyzeService> _logger;

        public AnalyzeService( IHttpClientFactory httpClientFactory, 
                               IOptions<FeedConfigSettings> feedConfigOptions,
                               ILogger<AnalyzeService> logger)
        {
            _clientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _feedConfigSettings = feedConfigOptions?.Value ?? throw new ArgumentNullException(nameof(feedConfigOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ReadStreamData()
        {
            //this class will allow us to create HttpClient instances but takes care of the
            //too many abandoned connections and stuff
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_feedConfigSettings.BearerToken}");
            var result = await client.GetStreamAsync(_feedConfigSettings.StreamUrl);
            Stopwatch s = new Stopwatch();
            long totalTweets = 0;
            s.Start();
            _logger.LogInformation("Stop clock started");
            Parallel.Invoke(() =>
            {
                if (null != result)
                {
                    using StreamReader reader = new StreamReader(result, Encoding.UTF8);
                    while ((reader.ReadLine()) != null)
                    {
                        totalTweets++;
                        // console these numbers every minute.
                        if (s.Elapsed.Seconds == _feedConfigSettings.ReportInterval)
                        {
                            Console.WriteLine($"Total number of tweets received: {totalTweets}");
                            Console.WriteLine($"Average tweets per minute: {Math.Round((totalTweets / s.Elapsed.TotalSeconds) * ONE_MINUTE)}");
                        }

                        // restart the count and timer, after may be every 24 hours. 
                        if (s.Elapsed.Hours == _feedConfigSettings.ResetAfterHours)
                        {
                            totalTweets = 0;
                            s.Restart();
                            _logger.LogInformation($"reset count after {_feedConfigSettings.ResetAfterHours} hours");
                        }
                    }
                    _logger.LogWarning("no stream feed");
                    // build retry in subsequent  versions. 
                }
            });
             s.Stop();
            _logger.LogInformation("Stop clock stopped");
        }
    }
}
