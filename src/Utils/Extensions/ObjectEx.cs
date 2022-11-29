using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

internal static class ObjectEx
{
	public static string Serialize<T>(this T @this, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions) =>
		jsonTypeInfo != null
			? JsonSerializer.Serialize(@this, jsonTypeInfo)
			: JsonSerializer.Serialize(@this, jsonOptions);

	public static async Task<Stream> SerializeAsync<T>(this T @this, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct = default)
	{
		var stream = new MemoryStream();

		var task = jsonTypeInfo != null
			? JsonSerializer.SerializeAsync(stream, @this, jsonTypeInfo, ct)
			: JsonSerializer.SerializeAsync(stream, @this, jsonOptions, ct);

		await task.ConfigureAwait(false);

		stream.Position = 0;
		return stream;
	}
}
