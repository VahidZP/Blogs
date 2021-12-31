using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication1
{
    using System;

    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.EntityFrameworkCore;

    using WebApplication1.Data.Contexts;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogContext>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("BlogConnection"));
            });

            services.AddAuthentication(
                option =>
                    {
                        option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    }).AddCookie(
                option =>
                    {
                        option.LoginPath = "/login";
                        option.LogoutPath = "/logout";
                        option.SlidingExpiration = true;
                        option.ExpireTimeSpan = TimeSpan.FromDays(20);
                    });


            services.AddControllersWithViews();
            services.AddRouting(
                options =>
                    {
                        options.LowercaseUrls = true;
                        options.LowercaseQueryStrings = true;
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
