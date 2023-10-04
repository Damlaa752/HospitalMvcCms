using App.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient();

//builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connStr = builder.Configuration.GetConnectionString("DBConStr"); // Builder konfigürasyonu içerisinde "DBConStr" appsettings.json deðerini oku.

    options.UseSqlServer(connStr);
});

//builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(k =>
{
    k.LoginPath = "/Auth/";
    k.LogoutPath = "/Auth/";
    k.AccessDeniedPath = "/Auth/";
    k.Cookie.Name = "AuthenticationCookie";
    k.Cookie.MaxAge = TimeSpan.FromDays(1);
});

builder.Services.AddAuthorization(p =>
{
    p.AddPolicy("AdminPolicy", c => c.RequireClaim("Role", "Admin"));
    p.AddPolicy("DoctorPolicy", c => c.RequireClaim("Role", "Doctor"));
    p.AddPolicy("UserPolicy", c => c.RequireClaim("Role", "User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// JWT - Token ve Cookie araþtýr.
//MASTERKADIR
//app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Uygulama ayaða kalktýðýnda, belirtilen Database'i getir.

    var db = dbContext.Database;

    if (!await db.CanConnectAsync()) // Eðer ilgili database'yi bulamýyorsan 
    {
        await db.EnsureCreatedAsync();

    }
}


app.Run();
