using EBook.Business;
using EBook.Data;
using Ebook.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ebook.Common.Models.Entities;
using EBook.Data.Data;

namespace Ebook.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false) 
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor(); //this line is used to access the HttpContext in the services and controllers and
                                                       //this will be used to get the current user and other information from the HttpContext.

            
            // Register data dependencies
            builder.Services.AddDataDependencies();
            builder.Services.AddBusinessDependencies();
            builder.Services.AddWebDependencies();

            //session & cookies
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                //options.Cookie.IsEssential = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope()) //this line is used to create a scope for the services and this will be used to seed the data in the database for SeedData class and
                                                           //this will be used to create the roles and the admin user in the database.
            {
                var services = scope.ServiceProvider;
                SeedData.InitializeAsync(services).GetAwaiter().GetResult();
            }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            //app.MapControllerRoute(
            //    name: "areas",
            //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

           

            app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

            // Root goes to Admin/Home/Index (optional, if you want Admin as default homepage)
            //app.MapControllerRoute(
            //    name: "default_admin",
            //    pattern: "",
            //    defaults: new { area = "Admin", controller = "Home", action = "Index" });

            


            app.Run();
        }
    }
}
