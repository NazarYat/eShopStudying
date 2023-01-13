using eShopStudying.DataAccess;
using eShopStudying.DataAccess.Repository;
using eShopStudying.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using eShopStudying.Utility;
using Stripe;
using eShopStudying.DataAccess.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SQLDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default"),
    x => x.MigrationsAssembly("eShopStudying.DataAccess")
));

builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("StripeSettings")
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<SQLDBContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddAuthentication().AddFacebook(options => {
    options.AppId = "475697268090468";
    options.AppSecret = "bfd45f564d700b2b4b453e67aebd2f76";
});
builder.Services.ConfigureApplicationCookie(option => {
    option.LoginPath = $"/Identity/Account/Login";
    option.LogoutPath = $"/Identity/Account/Logout";
    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

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

StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeSettings:SecretKey").Get<string>();
SeedDatabase();
app.UseAuthentication();;

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        dbInitializer.Initialize();
    }
}