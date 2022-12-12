using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

internal static class UrlStreamEx
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<string> ReadToEndAsync(this UrlStream @this, CancellationToken ct)
	{
		await using var stream = await @this.HttpResponseMessage.ReadAsStreamAsync(ct)
			.ConfigureAwait(false);
		
		return await stream.ReadToEndAsync()
			.ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> DeserializeAsync<T>(this UrlStream @this, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct)
	{
		await using var stream = await @this.HttpResponseMessage.ReadAsStreamAsync(ct)
			.ConfigureAwait(false);
		
		return await stream.DeserializeAsync(jsonTypeInfo, jsonOptions, ct)
			.ConfigureAwait(false);
	}
}
