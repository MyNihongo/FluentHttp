namespace MyNihongo.FluentHttp;

public interface IHttpServiceWithOptions
{
	internal IHttpService HttpService { get; }

	internal HttpCallOptions Options { get; set; }
}