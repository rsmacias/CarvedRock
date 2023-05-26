using Serilog;

// See https://aka.ms/new-console-template for more information

var name = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", name)
    .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
    .WriteTo.Console()
    .CreateLogger();

try {
    Log.ForContext("Args", args)
       .Information("Starting program...");

    Console.WriteLine("Hello, World!");  // do some invoice generation

    Log.Information("Finished execution!");
} catch (Exception ex) {
    Log.Fatal(ex, "Some kind of exception occured");
} finally {
    Log.CloseAndFlush();
}
