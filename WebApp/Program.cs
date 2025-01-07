using Microsoft.EntityFrameworkCore;
using WebApp.Models.Movies;
using Microsoft.AspNetCore.Identity;
using WebApp.CustomIdentity;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        

        builder.Services.AddIdentityCore<CustomUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddSignInManager()
            .AddUserStore<CustomUserStore>()
            .AddDefaultTokenProviders();
        
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "Identity.Application"; // Dopasowanie nazwy do schematu
            options.LoginPath = "/Account/Login"; // Strona logowania
            options.AccessDeniedPath = "/Account/AccessDenied"; // Strona odmowy dostępu
        });


        
        
        builder.Services.AddAuthorization();

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<MoviesDbContext>(op =>
        {
            op.UseSqlite(builder.Configuration["MoviesDatabase:ConnectionString"]);
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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        

        app.Run();
    }
}