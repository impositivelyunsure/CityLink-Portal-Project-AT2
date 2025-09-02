// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllersWithViews();
// builder.Services.AddSingleton<SCP.Services.NavigationService>();

// var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();
// app.Use(async (context, next) =>
// {
//     context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
//     context.Response.Headers["Pragma"] = "no-cache";
//     context.Response.Headers["Expires"] = "0";
//     await next();
// });

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();



using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SCP.Services;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// ------------------- Services -------------------

// MVC controllers + views
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Navigation service
builder.Services.AddSingleton<NavigationService>();

// MongoDB settings + client
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// App-specific services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<BookingService>();

// API versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new QueryStringApiVersionReader("SMTAFE-api-version");
});

builder.Services.AddDistributedMemoryCache(); // In-memory cache for session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // More secure
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

// ------------------- Build App -------------------

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Disable caching
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});

app.UseAuthorization();
app.UseSession(); 

// Routes for MVC + API
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

// ------------------- Run -------------------
app.Run();
