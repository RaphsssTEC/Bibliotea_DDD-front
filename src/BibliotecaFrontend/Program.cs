using BibliotecaFrontend.Application.Services;
using BibliotecaFrontend.Infrastructure.Api;
using BibliotecaFrontend.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IBibliotecaApiClient, BibliotecaApiClient>((serviceProvider, client) =>
{
    var settings = serviceProvider
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<ApiSettings>>()
        .Value;

    client.BaseAddress = new Uri(settings.BaseUrl.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddScoped<ILivroAppService, LivroAppService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Livros}/{action=Index}/{id?}");

app.Run();
