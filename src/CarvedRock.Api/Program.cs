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
    .WriteTo.Console()
    .CreateLogger();

try {

    var builder = WebApplication.CreateBuilder(args);

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

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();    

} catch (Exception ex) {
    Log.Fatal(ex, "Application terminated unexpectedly");
} finally {
    Log.CloseAndFlush();
}

