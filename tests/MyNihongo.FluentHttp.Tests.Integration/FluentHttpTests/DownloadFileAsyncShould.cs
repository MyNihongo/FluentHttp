namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

public sealed class DownloadFileAsyncShould : FluentHttpTestsBase
{
	public DownloadFileAsyncShould(ITestOutputHelper testOutputHelper)
		: base(testOutputHelper)
	{
	}
	
	[Fact]
	public async Task DownloadFavicon()
	{
		var localFolderPath = GetLocalSaveDirectory();
		
		var result = await CreateFixture()
			.AppendPathSegment("favicon.ico")
			.DownloadFileAsync(localFolderPath);

		result
			.Should()
			.NotBeEmpty();

		File.Exists(result)
			.Should()
			.BeTrue();
		
		File.Delete(result);
	}

	private static string GetLocalSaveDirectory()
	{
		var basePath = AppContext.BaseDirectory;
		return Path.Combine(basePath, "Downloads");
	}
}
