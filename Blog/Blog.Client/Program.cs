using Blog.Client.AuthenticationStateSyncer;
using Blog.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

//var handler = new HttpClientHandler
//{
//    // Bypass SSL certificate validation
//    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
//};
builder.Services.AddHttpClient("BlogAppApi", c =>
{
    c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddSingleton<IPostServices, PostServices>();
builder.Services.AddMudServices();
await builder.Build().RunAsync();
