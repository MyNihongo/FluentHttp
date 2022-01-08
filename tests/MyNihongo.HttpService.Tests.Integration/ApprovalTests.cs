using System.Text.Encodings.Web;
using System.Text.Json;
using ApprovalTests;

namespace MyNihongo.HttpService.Tests.Integration;

internal static class ApprovalTests
{
	public static void VerifyJson(object obj)
	{
		var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		});

		Approvals.VerifyJson(json);
	}
}