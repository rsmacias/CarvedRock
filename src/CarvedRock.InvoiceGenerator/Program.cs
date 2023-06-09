﻿using Serilog;
using Microsoft.Extensions.Configuration;

// See https://aka.ms/new-console-template for more information
IConfiguration _config;
var name = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", name)
    .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
    .WriteTo.Console()
    .CreateLogger();

try {
    // http://bit.ly/default-builder-source
    _config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
        .AddJsonFile("appsettings.json", false)
        .AddEnvironmentVariables()
        .Build();

    var connectionString = _config.GetConnectionString("Db");
    var simpleProperty = _config.GetValue<string>("SimpleProperty");
    var nestedProp = _config.GetValue<string>("Inventory:NestedProperty");

    Log.ForContext("ConnectionString", connectionString)
       .ForContext("SimpleProperty", simpleProperty)
       .ForContext("Inventory:NestedProperty", nestedProp)
       .Information("Loaded console configuration!");

    Log.ForContext("Args", args)
       .Information("Starting program...");

    Console.WriteLine("Hello, World!");  // do some invoice generation

    Log.Information("Finished execution!");
} catch (Exception ex) {
    Log.Fatal(ex, "Some kind of exception occured");
} finally {
    Log.CloseAndFlush();
}
