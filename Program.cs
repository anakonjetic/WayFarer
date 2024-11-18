using Microsoft.EntityFrameworkCore;
using WayFarer.Controllers.Services;
using WayFarer.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<WeatherService>();

builder.Services.AddDbContext<WayFarerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("WayFarerDbContext"),
            opt => opt.MigrationsAssembly("WayFarer")));


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Trajanje sesije
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthorization();

app.MapRazorPages();

app.Run();
