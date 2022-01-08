using System.Text.Json;
using System.Threading.Tasks;
using MyNihongo.FluentHttp.Tests.Integration.Models;
using Serilog.Events;
using Xunit;

namespace MyNihongo.FluentHttp.Tests.Integration.HttpServiceTests;

public sealed class PostJsonAsyncShould : HttpServiceTestsBase
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
			.PostJsonAsync(data, options, PostCreateRecordContext.Default.PostCreateRecord, postContext.PostRecord);

		ApprovalTests.VerifyJson(result);
	}

	[Fact]
	public async Task PostDaaWithoutVerboseLogging()
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
			.PostJsonAsync(data, options, PostCreateRecordContext.Default.PostCreateRecord, postContext.PostRecord);

		ApprovalTests.VerifyJson(result);
	}
}