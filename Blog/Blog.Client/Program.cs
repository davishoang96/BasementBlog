using Blog.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var handler = new HttpClientHandler
{
    // Bypass SSL certificate validation
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};

builder.Services.AddHttpClient<IPostServices, PostServices>(c =>
{
    c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).ConfigurePrimaryHttpMessageHandler(() => handler); ;

await builder.Build().RunAsync();
