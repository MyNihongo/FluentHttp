﻿namespace MyNihongo.FluentHttp;

internal static class AuthUtils
{
	private const string AuthHeaderKey = "Authorization";

	public static HeaderModel BasicAuth(string username, string password)
	{
		var value = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
		return new HeaderModel(AuthHeaderKey, $"Basic {value}");
	}
}
