using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

internal static class UrlResponseEx
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<string> ReadToEndAsync(this UrlResponse @this, CancellationToken ct)
	{
		await using var stream = await @this.ReadAsStreamAsync(ct)
			.ConfigureAwait(false);
		
		return await stream.ReadToEndAsync()
			.ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> DeserializeAsync<T>(this UrlResponse @this, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct)
	{
		await using var stream = await @this.ReadAsStreamAsync(ct)
			.ConfigureAwait(false);
		
		return await stream.DeserializeAsync(jsonTypeInfo, jsonOptions, ct)
			.ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<Stream> ReadAsStreamAsync(this UrlResponse @this, CancellationToken ct) =>
		@this.HttpResponseMessage.ReadAsStreamAsync(ct);
}
