using NoteRepository.Components;
using NoteRepository.Components.Buttons;
using NoteRepository.MongoDatabase;

var config =
    new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true)
        .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Set log level to Information
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddScoped<ButtonService>();
builder.Services.AddScoped<NoteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
