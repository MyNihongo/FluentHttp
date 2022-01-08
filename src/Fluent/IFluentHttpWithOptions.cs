namespace MyNihongo.FluentHttp;

public interface IFluentHttpWithOptions
{
	internal IFluentHttp Http { get; }

	internal HttpCallOptions Options { get; set; }
}