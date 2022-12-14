using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

internal static class StreamEx
{
	public static async Task<string> ReadToEndAsync(this Stream @this)
	{
		using var streamReader = new StreamReader(@this);

		return await streamReader.ReadToEndAsync()
			.ConfigureAwait(false);
	}

	public static async ValueTask<T> DeserializeAsync<T>(this Stream @this, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct)
	{
		var valueTask = jsonTypeInfo != null
			? JsonSerializer.DeserializeAsync(@this, jsonTypeInfo, ct)
			: JsonSerializer.DeserializeAsync<T>(@this, jsonOptions, ct);

		return await valueTask.ConfigureAwait(false) ?? throw new NullReferenceException("Cannot deserialize");
	}
}
