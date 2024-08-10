using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorBlog.Client.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IPostServices, PostServices>();

await builder.Build().RunAsync();
