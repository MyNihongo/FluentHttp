namespace MyNihongo.FluentHttp;

internal sealed class StringContentExtended : StringContent
{
	public StringContentExtended(string content)
		: base(content)
	{
		Content = content;
	}

	public string Content { get; }
}
