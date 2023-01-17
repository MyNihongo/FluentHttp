using System.Text.Json;

namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

[UsesVerify]
public sealed class SendJsonAsyncShould : FluentHttpTestsBase
{
	public SendJsonAsyncShould(ITestOutputHelper testOutputHelper)
		: base(testOutputHelper)
	{
	}

	[Fact]
	public async Task SendGetWithOptions()
	{
		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		
		var result = await CreateFixture()
			.AppendPathSegment("comments")
			.AppendParameter("postId", 1)
			.SendJsonAsync<PostCommentRecord[]>(HttpMethod.Get, null, jsonOptions)
			.ToJsonStringAsync();
		
		await Verify(result);
	}
	
	[Fact]
	public async Task SendGetWithTypeInfo()
	{
		var result = await CreateFixture()
			.AppendPathSegment("comments")
			.AppendParameter("postId", 1)
			.SendJsonAsync(HttpMethod.Get, null, PostCommentRecordContext.Default.PostCommentRecordArray)
			.ToJsonStringAsync();
		
		await Verify(result);
	}
	
	[Fact]
	public async Task SendPostWithOptions()
	{
		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		
		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Japanese grammar",
			Body = "Verbs, adjectives and nouns"
		};
		
		var result = await CreateFixture()
			.AppendPathSegment("posts")
			.SendJsonAsync<PostCreateRecord>(HttpMethod.Post, data, jsonOptions)
			.ToJsonStringAsync();
		
		await Verify(result);
	}
	
	[Fact]
	public async Task SendPostWithTypeInfo()
	{
		var data = new PostCreateRecord
		{
			UserId = 2,
			Title = "Japanese grammar",
			Body = "Verbs, adjectives and nouns"
		};
		
		var result = await CreateFixture()
			.AppendPathSegment("posts")
			.SendJsonAsync(HttpMethod.Post, data, PostCreateRecordContext.Default.PostCreateRecord)
			.ToJsonStringAsync();
		
		await Verify(result);
	}
}
