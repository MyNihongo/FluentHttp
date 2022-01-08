namespace MyNihongo.HttpService;

public interface IHttpServiceWithOptions
{
	internal IHttpService HttpService { get; }

	internal HttpCallOptions Options { get; set; }
}