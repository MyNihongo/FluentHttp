namespace MyNihongo.FluentHttp;

internal sealed class UrlStream : IAsyncDisposable, IDisposable
{
	public UrlStream(Stream stream, string url)
	{
		Stream = stream;
		Url = url;
	}
	
	public Stream Stream { get; }
	
	public string Url { get; }
	
	public void Dispose() =>
		Stream.Dispose();

	public ValueTask DisposeAsync() =>
		Stream.DisposeAsync();
}
