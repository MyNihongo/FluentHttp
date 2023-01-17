using System.Text.Json.Serialization;

namespace MyNihongo.FluentHttp.Tests.Integration.Models;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(PostCommentRecord[]))]
internal partial class PostCommentRecordContext : JsonSerializerContext
{
}

public sealed record PostCommentRecord
{
	public int PostId { get; set; }
	
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
	
	public string Email { get; set; } = string.Empty;
	
	public string Body { get; set; } = string.Empty;
}
