using Serilog;
using CarvedRock.OrderProcessor;

var name = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", name)
    .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
    .WriteTo.Console()
    .CreateLogger();

try {

    Log.Information("Starting host");

    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .UseSerilog()  // <-- Add Serilog support
        .Build();

    host.Run();

} catch (Exception ex) {
    Log.Fatal(ex, "Host terminated unexpectedly");
} finally {
    Log.CloseAndFlush();
}
