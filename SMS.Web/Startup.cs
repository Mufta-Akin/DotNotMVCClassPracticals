using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SMS.Data.Services;

namespace SMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           
            // ** Add Cookie and Jwt Authentication using extension method **
            var secret = Configuration.GetValue<string>("JwtConfig:Secret");                     
            services.AddCookieAndJwtAuthentication(secret);

            // ** Add Cookie Authentication via extension method **
            //services.AddCookieAuthentication();

            // enable cors for webapi
            //services.AddCors();

            // configure instance of IStudentService with dependency injection system
            services.AddTransient<IStudentService,StudentServiceDb>();

            // configure MVC
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // IServiceProvider added to enable call to seeder
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // here we ensure our service is seeded with dummy data for development purposes
                ServiceSeeder.Seed(provider.GetService<IStudentService>());               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // configure cors to allow full cross origin access to webapi
            //app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            /* Enable site Authentication/Authorization */
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
