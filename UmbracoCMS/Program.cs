using Microsoft.AspNetCore.Hosting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

// if (app.Environment.IsProduction() && IsRunningInAzure())
// {
//     var path = Path.Combine(app.Environment.ContentRootPath, "App_Data");
//     Directory.CreateDirectory(path);
    
//     var port = Environment.GetEnvironmentVariable("PORT") ?? Environment.GetEnvironmentVariable("WEBSITES_PORT") ?? "8080";
//     app.Urls.Clear();
//     app.Urls.Add($"http://0.0.0.0:{port}");
// }

await app.RunAsync();

// bool IsRunningInAzure()
// {
//     return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));
// }