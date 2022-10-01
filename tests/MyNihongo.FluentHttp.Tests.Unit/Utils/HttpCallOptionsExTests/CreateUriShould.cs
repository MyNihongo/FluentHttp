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
	public void ReturnAbsoluteUriWithParameter()
	{
		const string baseAddress = "https://mynihongo.org/tracks",
			key = nameof(key), value = nameof(value);

		const string expected = $"{baseAddress}?{key}={value}";

		var fixture = new HttpCallOptions
		{
			BaseAddress = baseAddress,
			Parameters =
			{
				{ key, value }
			}
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
	public void ReturnAbsoluteUriWithParameters()
	{
		const string baseAddress = "https://mynihongo.org/tracks",
			key1 = nameof(key1), value1 = nameof(value1),
			key2 = nameof(key2), value2 = nameof(value2);

		const string expected = $"{baseAddress}?{key1}={value1}&{key2}={value2}";

		var fixture = new HttpCallOptions
		{
			BaseAddress = baseAddress,
			Parameters =
			{
				{ key1, value1 },
				{ key2, value2 }
			}
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
	public void ReturnAbsoluteUriWithSegmentsAndParameters()
	{
		const string baseAddress = "https://mynihongo.org/tracks",
			segment1 = nameof(segment1), segment2 = nameof(segment2),
			key1 = nameof(key1), value1 = nameof(value1),
			key2 = nameof(key2), value2 = nameof(value2);

		const string expected = $"{baseAddress}/{segment1}/{segment2}?{key1}={value1}&{key2}={value2}";

		var fixture = new HttpCallOptions
		{
			BaseAddress = baseAddress,
			PathSegments = { segment1, segment2 },
			Parameters =
			{
				{ key1, value1 },
				{ key2, value2 }
			}
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
	public void ReturnRelativePath()
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

	[Fact]
	public void ReturnRelativePathWithParameter()
	{
		const string segment1 = nameof(segment1), segment2 = nameof(segment2),
			key = nameof(key), value = nameof(value);

		const string expected = $"{segment1}/{segment2}?{key}={value}";

		var fixture = new HttpCallOptions
		{
			PathSegments = { segment1, segment2 },
			Parameters =
			{
				{ key, value }
			}
		};

		var result = fixture.CreateUri();

		result.OriginalString
			.Should()
			.Be(expected);

		result.IsAbsoluteUri
			.Should()
			.BeFalse();
	}

	[Fact]
	public void ReturnRelativePathWithParameters()
	{
		const string segment1 = nameof(segment1), segment2 = nameof(segment2),
			key1 = nameof(key1), value1 = nameof(value1),
			key2 = nameof(key2), value2 = nameof(value2);

		const string expected = $"{segment1}/{segment2}?{key1}={value1}&{key2}={value2}";

		var fixture = new HttpCallOptions
		{
			PathSegments = { segment1, segment2 },
			Parameters =
			{
				{ key1, value1 },
				{ key2, value2 }
			}
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
