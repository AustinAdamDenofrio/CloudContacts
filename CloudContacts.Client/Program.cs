using CloudContacts.Client;
using CloudContacts.Client.Services;
using CloudContacts.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
// HTTP Client
builder.Services.AddScoped(ps => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// InterFace
builder.Services.AddScoped<IContactDTOService, WASMContactDTOService>();
builder.Services.AddScoped<ICategoryDTOService, WASMCategoryDTOService>();

await builder.Build().RunAsync();
