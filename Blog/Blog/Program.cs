using Auth0.AspNetCore.Authentication;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog;
using Blog.AuthenticationStateSyncer;
using Blog.Components;
using Blog.Database;
using Blog.Services;
using Blog.Services.Modules;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;

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
var savePostApi = builder.Configuration["SavePostApi"];
var authority = builder.Configuration["Auth0_Domain"];


builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite($"Data Source={databaseSource}"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers(o =>
{
    o.Filters.Add(new ProducesAttribute("application/json"));
});

// Use https
builder.WebHost.UseUrls(baseUrl);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder =>
//        {
//            builder.WithOrigins(baseUrl)
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

builder.Services.AddHttpClient("BlogAppApi", client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<IApiClient>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlogAppApi");
    return new ApiClient(baseUrl, httpClient);
});


builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = authority;
    options.Audience = savePostApi;
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

app.MapGet("/backupDb", async (HttpContext context) =>
{
    var timestamp = DateTime.Now.ToString("ddMMyyyy-HHmmss");
    var filename = $"blazorblog-{timestamp}.db";

    if (!File.Exists(databaseSource))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Database file not found.");
        return;
    }

    context.Response.ContentType = "application/octet-stream";
    context.Response.Headers.Add("Content-Disposition", $"attachment; filename={filename}");
    await context.Response.SendFileAsync(databaseSource);

}).RequireAuthorization(policy => policy.RequireRole("Admin"));

app.UseAuthorization();
//app.UseCors("AllowSpecificOrigin"); // Apply CORS policy globally
app.Run();
