using FluentAssertions;
using Moq;

namespace MyNihongo.FluentHttp.Tests.Unit;

internal static class ItIs
{
	public static T Equivalent<T>(T param) =>
		It.Is<T>(x => IsEquivalent(x, param));

	public static bool IsEquivalent<T>(T param1, T param2)
	{
		param1
			.Should()
			.BeEquivalentTo(param2, opt => opt.WithStrictOrdering());

		return true;
	}
}