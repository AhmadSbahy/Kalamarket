using Kalamarket.Core.Convartor;
using Kalamarket.Core.Service.Classes;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
#region IOC
builder.Services.AddScoped<IUserSrvice, UserSrvice>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IViewRenderService, RenderViewToString>();
builder.Services.AddScoped<IAdminService, AdminSrvice>();
builder.Services.AddScoped<IProductService, ProductSrvice>();
builder.Services.AddScoped<IOrderSrvice, OrderSrvice>();
builder.Services.AddScoped<IBlogSrvice, BlogSrvice>();
builder.Services.AddScoped<IPermissionSrvice, PermissionSrvice>();
#endregion

#region DB Context
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<KalamarketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KalamarketConnectionStrings"));

});
#endregion

#region Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login";
        option.LogoutPath = "/Logout";
        option.ExpireTimeSpan = TimeSpan.FromDays(30);
    });

#endregion


var app = builder.Build();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/Error404");
    }
});

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


app.MapRazorPages();


#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
#pragma warning restore ASP0014 // Suggest using top level route registrations


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
