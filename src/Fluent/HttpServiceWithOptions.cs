namespace MyNihongo.HttpService;

internal sealed class HttpServiceWithOptions : IHttpServiceWithOptions
{
	private readonly IHttpService _httpService;

	public HttpServiceWithOptions(IHttpService httpService)
	{
		_httpService = httpService;
	}

	public HttpCallOptions Options { get; set; } = new();

	IHttpService IHttpServiceWithOptions.HttpService => _httpService;

	HttpCallOptions IHttpServiceWithOptions.Options
	{
		get => Options;
		set => Options = value;
	}
}