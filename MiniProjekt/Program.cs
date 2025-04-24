using System;
using System.Net.Http;
using Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MiniProjekt;
using Blazored.LocalStorage;
using MiniProjekt.Service; // Tilføj denne linje

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage(); // Tilføj denne linje

builder.Services.AddScoped<IAnnonceService,AnnonceServiceServer>();

await builder.Build().RunAsync();