namespace MyNihongo.FluentHttp;

public sealed record HttpCallOptions
{
	public List<string> PathSegments { get; } = new();

	public Dictionary<string, string> Headers { get; } = new();
}