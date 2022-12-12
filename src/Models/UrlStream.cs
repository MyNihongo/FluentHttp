namespace MyNihongo.FluentHttp;

internal sealed class UrlStream : IDisposable
{
	public UrlStream(HttpResponseMessage httpResponseMessage, string url)
	{
		HttpResponseMessage = httpResponseMessage;
		Url = url;
	}

	public HttpResponseMessage HttpResponseMessage { get; }
	
	public string Url { get; }
	
	public void Dispose() =>
		HttpResponseMessage.Dispose();
}
