using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UstunTicaretDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Admin/Login/Index";
		options.AccessDeniedPath = "/Admin/Login/Denied";
	});

builder.Services.AddAuthorization(options =>
{
	options.DefaultPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
		.RequireAuthenticatedUser()
		.Build();
});



var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute("product_in_category", "product/{category}", new { controller = "home", action = "product" });
app.MapControllerRoute("product_in_brand", "product/brand/{brand}", new { controller = "home", action = "product" });

app.MapDefaultControllerRoute();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});

app.Run();
