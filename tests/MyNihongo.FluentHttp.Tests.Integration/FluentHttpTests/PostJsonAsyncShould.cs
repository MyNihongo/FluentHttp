using System.Text.Json;

namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

[UsesVerify]
public sealed class PostJsonAsyncShould : FluentHttpTestsBase
{
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
}
