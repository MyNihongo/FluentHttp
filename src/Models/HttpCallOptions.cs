namespace MyNihongo.FluentHttp;

public sealed record HttpCallOptions
{
	public string BaseAddress { get; set; } = string.Empty;

	public List<string> PathSegments { get; } = new();

	public Dictionary<string, string> Headers { get; } = new();
}
