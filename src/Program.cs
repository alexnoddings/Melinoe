using Blazored.LocalStorage;
using Melinoe.Game;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<GameCollection>();
builder.Services.AddSingleton<WikiLinkService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "");
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
