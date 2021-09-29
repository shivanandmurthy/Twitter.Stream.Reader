using System.Threading.Tasks;

namespace Twitter.Stream.Reader.Core
{
	public interface IAnalyzeService
	{
		public Task ReadStreamData();
	}
}
