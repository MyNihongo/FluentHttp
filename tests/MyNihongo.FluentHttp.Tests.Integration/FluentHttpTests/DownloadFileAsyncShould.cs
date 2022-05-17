using Flurl.Http;

namespace MyNihongo.FluentHttp.Tests.Integration.FluentHttpTests;

public sealed class DownloadFileAsyncShould : FluentHttpTestsBase
{
	public DownloadFileAsyncShould()
		: base("dayton")
	{
	}

	[Fact]
	public async Task DownloadWithVerboseLogging()
	{
		const string downloadDir = "Downloads";

		var result = await CreateFixture()
			.AppendPathSegments("wp-content", "themes", "mtoy", "img", "logo1.png")
			.DownloadFileAsync(downloadDir);

		await Verify(result);
	}
}
