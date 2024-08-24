using Auth0.AspNetCore.Authentication;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Blog;
using Blog.AuthenticationStateSyncer;
using Blog.Components;
using Blog.Database;
using Blog.Services;
using Blog.Services.Modules;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new ServiceModules());
            builder.RegisterModule(new RepositoryModule());
        });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();


// Variables
var baseUrl = builder.Configuration["BaseUrl"];
var databaseSource = builder.Configuration["DatabaseSource"];
var portNumber = int.Parse(builder.Configuration["PortNumber"]);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite($"Data Source={databaseSource}"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls(baseUrl);
}
else
{
    var pem = builder.Configuration["pemFilePath"];
    var pemKey = builder.Configuration["pemKey"];
    var x509 = X509Certificate2.CreateFromPemFile(pem, pemKey);

    builder.WebHost.ConfigureKestrel(o =>
    {
        o.ListenAnyIP(portNumber, lo =>
        {
            lo.UseHttps(x509);
        });
    });
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(baseUrl)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

builder.Services.AddHttpClient<IApiClient, ApiClient>("BlogAppApi", client =>
{
    client.BaseAddress = new Uri(baseUrl);
}).AddHttpMessageHandler<TokenHandler>();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddMudServices();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My BlogApp Api",
    });
});

var app = builder.Build();

// Apply migration
using (var scope = ((IApplicationBuilder)app).ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My BlogApp Api");
        c.RoutePrefix = "swagger"; // Set Swagger UI at the app's root
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Blog.Client._Imports).Assembly);

app.MapGet("/Account/Login", async (HttpContext httpContext, string returnUrl = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext) =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri("/")
            .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});
app.UseCors("AllowSpecificOrigin"); // Apply CORS policy globally
app.Run();
