using Blog.Client.AuthenticationStateSyncer;
using Blog.Services;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("BlogAppApi", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddScoped<IApiClient>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlogAppApi");
    return new ApiClient(builder.HostEnvironment.BaseAddress, httpClient);
});

builder.Services.AddScoped<IMarkdownService, MarkdownService>();
builder.Services.AddMudServices();
await builder.Build().RunAsync();
