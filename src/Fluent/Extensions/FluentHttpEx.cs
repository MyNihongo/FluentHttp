namespace MyNihongo.FluentHttp;

public static class FluentHttpEx
{
	public static IFluentHttpWithOptions AppendPathSegment(this IFluentHttp @this, string pathSegment) =>
		new FluentHttpWithOptions(@this)
		{
			Options = new HttpCallOptions
			{
				PathSegments = { pathSegment }
			}
		};

	public static IFluentHttpWithOptions AppendPathSegments(this IFluentHttp @this, params string[] pathSegments)
	{
		var result = new FluentHttpWithOptions(@this);
		result.Options.PathSegments.AddRange(pathSegments);

		return result;
	}

	public static IFluentHttpWithOptions WithHeader(this IFluentHttp @this, string header, string value) =>
		new FluentHttpWithOptions(@this)
		{
			Options = new HttpCallOptions
			{
				Headers =
				{
					{ header, value }
				}
			}
		};

	public static IFluentHttpWithOptions WithBasicAuth(this IFluentHttp @this, string username, string password)
	{
		var (header, value) = AuthUtils.BasicAuth(username, password);
		return @this.WithHeader(header, value);
	}
}