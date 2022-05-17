namespace MyNihongo.FluentHttp.Tests.Unit.Utils.UriExTests;

public sealed class GetAbsoluteUriShould
{
	[Fact]
	public void CorrectlyCombineUri()
	{
		Uri fixture = new("https://test.my.jp/namespaces/AXSF/", UriKind.Absolute),
			input = new("/filemanagement/ecb51c11-33d3-47c2-8ccd-4477686544c5?access_token=val", UriKind.Relative);

		const string expected = "https://test.my.jp/namespaces/AXSF/filemanagement/ecb51c11-33d3-47c2-8ccd-4477686544c5?access_token=val";

		var result = fixture.GetAbsoluteUri(input);

		result
			.Should()
			.Be(expected);
	}
}
