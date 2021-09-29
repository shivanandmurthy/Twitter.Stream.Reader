namespace Twitter.Stream.Reader.Infrastructure.model
{
	public class FeedConfigSettings
	{
		public string BearerToken { get; set; }
		public string StreamUrl { get; set; }
		public int ReportInterval { get; set; }
		public int ResetAfterHours { get; set; }
	}
}
