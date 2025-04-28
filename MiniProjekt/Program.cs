using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiniProjekt;
using MiniProjekt.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Tilføj services fra begge grene
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ILoginService, LoginServiceServer>();
builder.Services.AddSingleton<IAnnonceService, AnnonceServiceServer>();

await builder.Build().RunAsync();

