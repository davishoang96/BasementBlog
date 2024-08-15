using Blog.Client.Services;
using Blog.Components;
using Blog.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Variables
var baseUrl = builder.Configuration["BaseUrl"];

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Data Source=BlogDatabase.db"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
builder.WebHost.UseUrls(baseUrl);

var handler = new HttpClientHandler
{
    // Bypass SSL certificate validation
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};

builder.Services.AddHttpClient("BlogAppApi", c =>
{
    c.BaseAddress = new Uri(baseUrl);
}).ConfigurePrimaryHttpMessageHandler(() => handler);


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

app.Run();
