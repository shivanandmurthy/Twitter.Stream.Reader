namespace Twitter.Stream.Reader.Infrastructure
{
	public interface IDbInitializer
    {
        void Initialize();
        void EnsureMigration();
    }
}
