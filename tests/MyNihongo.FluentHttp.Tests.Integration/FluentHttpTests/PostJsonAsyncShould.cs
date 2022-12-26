using System.Text.Json;

namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

[UsesVerify]
public sealed class PostJsonAsyncShould : FluentHttpTestsBase
{
	public PostJsonAsyncShould(ITestOutputHelper testOutputHelper)
		: base(testOutputHelper)
	{
	}
	
	[Fact]
	public async Task PostDataWithVerboseLogging()
	{
		var options = new HttpCallOptions
		{
			PathSegments = { "posts" }
		};

		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Japanese grammar",
			Body = "Verbs, adjectives and nouns"
		};

		var postContext = new PostRecordContext(new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		var result = await CreateFixture()
			.PostJsonAsync(data, options, PostCreateRecordContext.Default.PostCreateRecord, postContext.PostRecord)
			.ToJsonStringAsync();

		await Verify(result);
	}

	[Fact]
	public async Task PostDataWithoutVerboseLogging()
	{
		LogLevel = LogEventLevel.Information;

		var options = new HttpCallOptions
		{
			PathSegments = { "posts" }
		};

		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Japanese grammar",
			Body = "Verbs, adjectives and nouns"
		};

		var postContext = new PostRecordContext(new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		var result = await CreateFixture()
			.PostJsonAsync(data, options, PostCreateRecordContext.Default.PostCreateRecord, postContext.PostRecord)
			.ToJsonStringAsync();

		await Verify(result);
	}

	[Fact]
	public async Task RetrieveInvalidModel()
	{
		var options = new HttpCallOptions
		{
			PathSegments = { "posts" }
		};

		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Japanese grammar",
			Body = "Verbs, adjectives and nouns"
		};

		var postContext = new UserRecordContext(new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		var result = await CreateFixture()
			.PostJsonOrDefaultAsync(data, options, PostCreateRecordContext.Default.PostCreateRecord, postContext.UserRecordArray);

		result
			.Should()
			.BeNull();
	}

	[Fact]
	public async Task PostWithJsonOptions()
	{
		LogLevel = LogEventLevel.Information;

		var options = new HttpCallOptions
		{
			PathSegments = { "posts" }
		};

		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Kanji",
			Body = "I love kanji more than katakana"
		};

		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		var result = await CreateFixture()
			.PostJsonAsync<PostCreateRecord, PostRecord>(data, options, jsonOptions)
			.ToJsonStringAsync();

		await Verify(result);
	}
	
	[Fact]
	public async Task ThrowIfNotFound()
	{
		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Kanji",
			Body = "I love kanji more than katakana"
		};

		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		
		var action = async () => await CreateFixture()
			.AppendPathSegment("not-exists")
			.PostJsonAsync<PostCreateRecord, PostRecord>(data, jsonOptions);

		await action
			.Should()
			.ThrowExactlyAsync<HttpCallException>();
	}

	[Fact]
	public async Task ReturnNullIfNotFoundWithTryPost()
	{
		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Kanji",
			Body = "I love kanji more than katakana"
		};

		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		var result = await CreateFixture()
			.AppendPathSegment("not-exists")
			.PostJsonOrDefaultAsync<PostCreateRecord, PostRecord>(data, jsonOptions);

		result
			.Should()
			.BeNull();
	}
}
