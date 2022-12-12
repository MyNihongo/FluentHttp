using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.FluentHttp;

internal static class UrlStreamEx
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<string> ReadToEndAsync(this UrlStream @this) =>
		@this.Stream.ReadToEndAsync();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTask<T> DeserializeAsync<T>(this UrlStream @this, JsonTypeInfo<T>? jsonTypeInfo, JsonSerializerOptions? jsonOptions, CancellationToken ct) =>
		@this.Stream.DeserializeAsync(jsonTypeInfo, jsonOptions, ct);
}
