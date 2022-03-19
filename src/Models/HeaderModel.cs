namespace MyNihongo.FluentHttp;

internal readonly ref struct HeaderModel
{
	public HeaderModel(string key, string value)
	{
		Key = key;
		Value = value;
	}

	public string Key { get; }

	public string Value { get; }

	public void Deconstruct(out string key, out string value)
	{
		key = Key;
		value = Value;
	}
}
