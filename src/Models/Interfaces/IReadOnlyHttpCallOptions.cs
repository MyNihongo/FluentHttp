namespace MyNihongo.FluentHttp;

public interface IReadOnlyHttpCallOptions
{
	public string BaseAddress { get; }

	public IReadOnlyList<string> PathSegments { get; }

	public IReadOnlyDictionary<string, string> Parameters { get; }

	public IReadOnlyDictionary<string, string> Headers { get; }
}
