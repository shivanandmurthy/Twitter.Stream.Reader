using System.Net.Http;
using System.Threading.Tasks;
using Twitter.Stream.Reader.Core;
using Xunit;

namespace Twitter.Stream.Reader.Test
{
	public class AnalyzeServiceTest : IAnalyzeService
    {
        [Fact]
        public async Task ReadStreamData()
        {
            // to do . twitter moq objects in next version.
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TestSeedData.bearerToken}");
            var result = await _client.GetStreamAsync(TestSeedData.url);
            Assert.NotNull(result);
        }
    }
}
