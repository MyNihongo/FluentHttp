global using System.Text.Json;
global using System.Text.Json.Serialization;
global using MyNihongo.FluentHttp.Tests.Integration.Models;
global using Serilog.Events;
global using Xunit;

[assembly: ApprovalTests.Reporters.UseReporter(typeof(ApprovalTests.Reporters.QuietReporter))]
[assembly: ApprovalTests.Namers.UseApprovalSubdirectory("Approvals")] 