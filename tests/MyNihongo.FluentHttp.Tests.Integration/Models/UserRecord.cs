using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
		private const string LatitudeName = "lat",
			LongitudeName = "lng";

		[JsonPropertyName(LatitudeName)]
		[JsonProperty(LatitudeName)]
		public string Latitude { get; set; } = string.Empty;

		[JsonPropertyName(LongitudeName)]
		[JsonProperty(LongitudeName)]
		public string Longitude { get; set; } = string.Empty;
	}

	public sealed record CompanyRecord
	{
		private const string BusinessTypeName = "bs";

		public string Name { get; set; } = string.Empty;

		public string CatchPhrase { get; set; } = string.Empty;

		[JsonPropertyName(BusinessTypeName)]
		[JsonProperty(BusinessTypeName)]
		public string BusinessType { get; set; } = string.Empty;
	}
}
