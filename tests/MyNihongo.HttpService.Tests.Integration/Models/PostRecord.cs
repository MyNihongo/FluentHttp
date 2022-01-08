using System.Text.Json.Serialization;

namespace MyNihongo.HttpService.Tests.Integration.Models;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(PostRecord[]))]
internal partial class PostRecordContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(PostCreateRecord))]
internal partial class PostCreateRecordContext : JsonSerializerContext
{
}

public abstract record PostRecordBase
{
	public int UserId { get; set; }

	public string Title { get; set; } = string.Empty;

	public string Body { get; set; } = string.Empty;
}

public sealed record PostRecord : PostRecordBase
{
	public int Id { get; set; }
}

public sealed record PostCreateRecord : PostRecordBase;