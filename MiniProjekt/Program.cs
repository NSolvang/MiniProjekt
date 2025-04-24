using System;
using System.Net.Http;
using Blazored.LocalStorage;
using Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MiniProjekt;
using MiniProjekt.Service;
using SwapClothes.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Tilføj services fra begge grene
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ILoginService, LoginServiceClientSlide>();
builder.Services.AddScoped<IAnnonceService, AnnonceServiceServer>();

await builder.Build().RunAsync();
