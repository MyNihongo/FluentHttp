using System.Net;

namespace MyNihongo.FluentHttp;

public sealed class HttpCallException : Exception
{
	public HttpCallException(HttpStatusCode statusCode, string content)
	{
		StatusCode = statusCode;
		Content = content;
	}

	public HttpStatusCode StatusCode { get; }

	public string Content { get; }
}