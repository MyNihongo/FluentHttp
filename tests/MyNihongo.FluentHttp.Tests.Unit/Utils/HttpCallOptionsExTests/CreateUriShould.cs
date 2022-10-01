namespace MyNihongo.FluentHttp.Tests.Unit.Utils.HttpCallOptionsExTests;

public sealed class CreateUriShould
{
	[Fact]
	public void ReturnAbsoluteUriWithoutSegments()
	{
		const string baseAddress = "https://mynihongo.org/tracks";

		var fixture = new HttpCallOptions
		{
			BaseAddress = baseAddress
		};

		var result = fixture.CreateUri();

		result.AbsoluteUri
			.Should()
			.Be(baseAddress);

		result.IsAbsoluteUri
			.Should()
			.BeTrue();
	}

	[Fact]
	public void ReturnAbsoluteUriWithSegments()
	{
		const string baseAddress = "https://mynihongo.org/tracks",
			segment1 = nameof(segment1), segment2 = nameof(segment2);

		const string expected = $"{baseAddress}/{segment1}/{segment2}";

		var fixture = new HttpCallOptions
		{
			BaseAddress = baseAddress,
			PathSegments = { segment1, segment2 }
		};

		var result = fixture.CreateUri();

		result.AbsoluteUri
			.Should()
			.Be(expected);

		result.IsAbsoluteUri
			.Should()
			.BeTrue();
	}

	[Fact]
	public void ReturnRelativePathWithoutSegments()
	{
		const string segment1 = nameof(segment1), segment2 = nameof(segment2);

		const string expected = $"{segment1}/{segment2}";

		var fixture = new HttpCallOptions
		{
			PathSegments = { segment1, segment2 }
		};

		var result = fixture.CreateUri();

		result.OriginalString
			.Should()
			.Be(expected);

		result.IsAbsoluteUri
			.Should()
			.BeFalse();
	}
}
