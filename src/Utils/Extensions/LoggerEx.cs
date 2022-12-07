using System.Net;
using Microsoft.Extensions.Logging;

namespace MyNihongo.FluentHttp;

internal static partial class LoggerEx
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Trace,
		Message = "-REQUEST-\nMethod: {method}\nURL: {absoluteUrl}",
		SkipEnabledCheck = true
	)]
	public static partial void LogRequest(this ILogger logger, in HttpMethod method, in string absoluteUrl);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Trace,
		Message = "-REQUEST-\nMethod: {method}\nURL: {absoluteUrl}\nContent: {stringData}",
		SkipEnabledCheck = true
	)]
	public static partial void LogRequest(this ILogger logger, in HttpMethod method, in string absoluteUrl, in string stringData);

	[LoggerMessage(
		EventId = 3,
		Level = LogLevel.Debug,
		Message = "{statusCodeName} ({statusCodeNumber}). Request time: {timeSpan}",
		SkipEnabledCheck = true
	)]
	public static partial void LogRequestTime(this ILogger logger, in HttpStatusCode statusCodeName, in int statusCodeNumber, in TimeSpan timeSpan);

	[LoggerMessage(
		EventId = 4,
		Level = LogLevel.Trace,
		Message = "-RESPONSE-\nURL: {absoluteUrl}\nContent: {stringData}",
		SkipEnabledCheck = true
	)]
	public static partial void LogResponse(this ILogger logger, in string absoluteUrl, in string stringData);

	[LoggerMessage(
		EventId = 5,
		Level = LogLevel.Trace,
		Message = "-RESPONSE ERROR-\nURL: {absoluteUrl}\nContent: {stringData}",
		SkipEnabledCheck = true
	)]
	public static partial void LogResponseError(this ILogger logger, in string absoluteUrl, in string stringData);

	[LoggerMessage(
		EventId = 6,
		Level = LogLevel.Warning,
		Message = "Cannot deserialize JSON: {message}",
		SkipEnabledCheck = true
	)]
	public static partial void LogJsonSerializationFailed(this ILogger logger, in string message);
}
