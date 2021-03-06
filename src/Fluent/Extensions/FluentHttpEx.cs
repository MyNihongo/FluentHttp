namespace MyNihongo.FluentHttp;

public static class FluentHttpEx
{
	/// <summary>
	/// Appends <see cref="pathSegment"/> to the base URI of the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="pathSegment">Path segment added to the base URI</param>
	public static IFluentHttpWithOptions AppendPathSegment(this IFluentHttp @this, string pathSegment) =>
		new FluentHttpWithOptions(@this)
		{
			Options = new HttpCallOptions
			{
				PathSegments = { pathSegment }
			}
		};

	/// <summary>
	/// Appends <see cref="pathSegments"/> to the base URI of the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="pathSegments">Path segments added to the base URI</param>
	public static IFluentHttpWithOptions AppendPathSegments(this IFluentHttp @this, params string[] pathSegments)
	{
		var result = new FluentHttpWithOptions(@this);
		result.Options.PathSegments.AddRange(pathSegments);

		return result;
	}

	/// <summary>
	/// Adds the <see cref="header"/> with the <see cref="value"/> to the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="header">Key of the header</param>
	/// <param name="value">Value of the header</param>
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

	/// <summary>
	/// Adds the Basic Authentication header with the <see cref="username"/> and the <see cref="password"/> to the HTTP call
	/// </summary>
	/// <param name="this">Instance of the HTTP client provider</param>
	/// <param name="username">Username of the authenticating user</param>
	/// <param name="password">Password of the authenticating user</param>
	public static IFluentHttpWithOptions WithBasicAuth(this IFluentHttp @this, string username, string password)
	{
		var (header, value) = AuthUtils.BasicAuth(username, password);
		return @this.WithHeader(header, value);
	}
}
