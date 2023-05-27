using Serilog;
using CarvedRock.Api.Interfaces;
using CarvedRock.Api.Domain;
using CarvedRock.Api.Extensions;

var name = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", name)
    // https://github.com/serilog/serilog/wiki/provided-sinks
    // https://datalust.co/seq
    // https://docs.datalust.co/docs/getting-started-with-docker
    .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
    .WriteTo.Console()
    .CreateLogger();

try {

    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("Db");
    var simpleProperty = builder.Configuration.GetValue<string>("SimpleProperty");
    var nestedProp = builder.Configuration.GetValue<string>("Inventory:NestedProperty");

    Log.ForContext("ConnectionString", connectionString)
       .ForContext("SimpleProperty", simpleProperty)
       .ForContext("Inventory:NestedProperty", nestedProp)
       .Information("Loaded configuration!");

    
    builder.Host.UseSerilog(); // <-- Add Serilog support

    // Add services to the container.
    builder.Services.AddScoped<IProductLogic, ProductLogic>();
    builder.Services.AddScoped<IQuickOrderLogic, QuickOrderLogic>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseCustomExceptionHandler();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCustomRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();    

} catch (Exception ex) {
    Log.Fatal(ex, "Application terminated unexpectedly");
} finally {
    Log.CloseAndFlush();
}

