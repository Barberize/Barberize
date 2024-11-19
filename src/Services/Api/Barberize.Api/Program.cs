using Barberize.Api;
using Barberize.Shared.Extensions;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Assemblies
var settingsAssembly = typeof(SettingsModule).Assembly;

// Add services to the container

// Swagger/OpenAPI: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Exceptions
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add Mediatr handlers and FluentValidation validators
builder.Services.AddMediatrAndValidatorsWithAssemblies(
    settingsAssembly);

// Add Api endpoints using Carter
builder.Services.AddCarterWithAssemblies(
    settingsAssembly);

// Add modules
builder.Services
    .AddSettingsModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.UseExceptionHandler(options => { });

// Use modules
app.UseSettingsModule();

app.Run();
