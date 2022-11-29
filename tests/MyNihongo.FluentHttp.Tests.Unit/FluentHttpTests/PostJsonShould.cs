using System.Text.Json;

namespace MyNihongo.FluentHttp.Tests.Unit.FluentHttpTests;

public sealed class PostJsonShould : FluentHttpTestsBase
{
	[Fact]
	public async Task PostJson()
	{
		const string pathSegment = nameof(pathSegment);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPost(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task PostJsonOrDefault()
	{
		const string pathSegment = nameof(pathSegment);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.PostJsonOrDefaultAsync<RequestRecord, ResponseRecord>(req, ct: cts.Token);

		VerifyPostOrDefault(req, expectedOptions, cts.Token);
	}

	[Fact]
	public async Task PostJsonWithOptions()
	{
		const string pathSegment = nameof(pathSegment);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		var jsonOptions = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.PostJsonAsync<RequestRecord, ResponseRecord>(req, jsonOptions, ct: cts.Token);

		VerifyPost(req, expectedOptions, jsonOptions, cts.Token);
	}
	
	[Fact]
	public async Task PostJsonOrDefaultWithOptions()
	{
		const string pathSegment = nameof(pathSegment);

		var expectedOptions = new HttpCallOptions
		{
			PathSegments = { pathSegment }
		};

		var req = new RequestRecord { Id = 1 };
		using var cts = new CancellationTokenSource();

		var jsonOptions = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		await CreateFixture()
			.AppendPathSegment(pathSegment)
			.PostJsonOrDefaultAsync<RequestRecord, ResponseRecord>(req, jsonOptions, ct: cts.Token);

		VerifyPostOrDefault(req, expectedOptions, jsonOptions, cts.Token);
	}
}
