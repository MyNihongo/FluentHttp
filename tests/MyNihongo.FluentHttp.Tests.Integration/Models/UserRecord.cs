using System.Text.Json.Serialization;

namespace MyNihongo.FluentHttp.Tests.Integration.Models;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(UserRecord[]))]
internal partial class UserRecordContext : JsonSerializerContext
{
}

public sealed record UserRecord
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Username { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public AddressRecord Address { get; set; } = new();

	public string Phone { get; set; } = string.Empty;

	public string Website { get; set; } = string.Empty;

	public CompanyRecord Company { get; set; } = new();

	public sealed record AddressRecord
	{
		public string Street { get; set; } = string.Empty;

		public string Suite { get; set; } = string.Empty;

		public string City { get; set; } = string.Empty;

		public string Zipcode { get; set; } = string.Empty;

		public GeoRecord Geo { get; set; } = new();
	}

	public sealed record GeoRecord
	{
		[JsonPropertyName("lat")]
		public string Latitude { get; set; } = string.Empty;

		[JsonPropertyName("lng")]
		public string Longitude { get; set; } = string.Empty;
	}

	public sealed record CompanyRecord
	{
		public string Name { get; set; } = string.Empty;

		public string CatchPhrase { get; set; } = string.Empty;

		[JsonPropertyName("bs")]
		public string BusinessType { get; set; } = string.Empty;
	}
}