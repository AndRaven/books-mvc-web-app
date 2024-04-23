using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BooksWebApp.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcBookContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcBookContext") ?? throw new InvalidOperationException("Connection string 'MvcBookContext' not found.")));

//use SQLite in Development and SLQ Server in Production
// if (builder.Environment.IsDevelopment())
// {
//     builder.Services.AddDbContext<MvcBookContext>(options =>
//         options.UseSqlite(builder.Configuration.GetConnectionString("MvcBookContext")));
// }
// else
// {
//     builder.Services.AddDbContext<MvcBookContext>(options =>
//         options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMvcBookContext")));
// }

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the HTTP request pipeline
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IBookService, BookService>();

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IBookService, BookService>();

//add HTTO Client to invoke Books.API
builder.Services.AddHttpClient("BooksAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BooksAPI:BaseAddress") ??
                                throw new InvalidOperationException("BooksAPI:BaseAddress configuration not found."));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
