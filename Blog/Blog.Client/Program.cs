using Blog.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//var handler = new HttpClientHandler
//{
//    // Bypass SSL certificate validation
//    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
//};
builder.Services.AddHttpClient("test", c =>
{
    c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddSingleton<IPostServices, PostServices>();

await builder.Build().RunAsync();
