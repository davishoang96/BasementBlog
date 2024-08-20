using Auth0.AspNetCore.Authentication;
using Blog.Client.Services;
using Blog.Components;
using Blog.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;
using Blog.AuthenticationStateSyncer;
using Microsoft.AspNetCore.Components.Authorization;
using AutoFixture;
using Blog.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

// Variables
var baseUrl = builder.Configuration["BaseUrl"];

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Data Source=BlogDatabase.db"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
if (builder.Environment.EnvironmentName != "Development")
{
    var pem = builder.Configuration["pemFilePath"];
    var pemKey = builder.Configuration["pemKey"];
    var x509 = X509Certificate2.CreateFromPem(pem, pemKey);
    builder.WebHost.ConfigureKestrel(o =>
    {
        o.ListenAnyIP(5000, lo =>
        {
            lo.UseHttps(x509);
        });
    });
}
else
{
    builder.WebHost.UseUrls(baseUrl);
}

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

builder.Services.AddHttpClient("BlogAppApi",
      client => client.BaseAddress = new Uri(baseUrl))
      .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("BlogAppApi"));

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddSingleton<IPostServices, PostServices>();

builder.Services.AddMudServices();

builder.Services.AddSwaggerGen(c =>
{
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
        c.RoutePrefix = "api-docs"; // Set Swagger UI at the app's root
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

app.Run();
