namespace MyNihongo.FluentHttp;

internal sealed class FluentHttpWithOptions : IFluentHttpWithOptions
{
	private readonly IFluentHttp _fluentHttp;

	public FluentHttpWithOptions(IFluentHttp fluentHttp)
	{
		_fluentHttp = fluentHttp;
	}

	public HttpCallOptions Options { get; set; } = new();

	IFluentHttp IFluentHttpWithOptions.Http => _fluentHttp;

	HttpCallOptions IFluentHttpWithOptions.Options
	{
		get => Options;
		set => Options = value;
	}
}