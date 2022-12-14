namespace MyNihongo.FluentHttp;

internal sealed class UrlResponse : IDisposable
{
	public UrlResponse(HttpResponseMessage httpResponseMessage, string url)
	{
		HttpResponseMessage = httpResponseMessage;
		Url = url;
	}

	public HttpResponseMessage HttpResponseMessage { get; }
	
	public string Url { get; }
	
	public void Dispose() =>
		HttpResponseMessage.Dispose();
}
