using Blog.Client.AuthenticationStateSyncer;
using Blog.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddHttpClient("BlogAppApi", c =>
{
    c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// Register the ApiClient with both the baseUrl and HttpClient
builder.Services.AddScoped<IApiClient>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlogAppApi");
    return new ApiClient(builder.HostEnvironment.BaseAddress, httpClient);
});

builder.Services.AddMudServices();
await builder.Build().RunAsync();
