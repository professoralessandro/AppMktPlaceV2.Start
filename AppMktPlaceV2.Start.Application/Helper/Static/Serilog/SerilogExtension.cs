#region IMPORT
using Serilog.Events;
using Serilog.Filters;
using Serilog;
using Serilog.Exceptions;
using Microsoft.Extensions.Configuration;
#endregion IMPORT

namespace AppMktPlaceV2.Start.Application.Helper.Static.Serilog
{
    public static class SerilogExtension
    {
        public static void AddSerilogApi(this IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationId()
                .Enrich.WithProperty("ApplicationName", $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .WriteTo.Async(wt => wt.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .CreateLogger();
        }
    }
}
