using CloudContacts.Client;
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

await builder.Build().RunAsync();
