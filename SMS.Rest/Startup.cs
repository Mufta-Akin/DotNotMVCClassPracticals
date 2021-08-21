using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using SMS.Data.Services;

namespace SMS.Rest
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
            // consider use of strongly typed configuration
            //https://weblog.west-wind.com/posts/2016/may/23/strongly-typed-configuration-settings-in-aspnet-core

            // configure jwt authentication using extension method            
            services.AddJwtAuthentication(Configuration.GetValue<string>("JwtConfig:Secret"));

            // configure instance of IStudentService with dependency injection system
            services.AddTransient<IStudentService,StudentServiceDb>();

            // add option to handle cross origin requests
            services.AddCors();            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SMS.Rest", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // here we ensure our service is seeded with dummy data for development purposes
                ServiceSeeder.Seed(new StudentServiceDb());       
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SMS.Rest v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            // configure cors to allow full access
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
         
            /* Enable site Authentication/Authorization */
            app.UseAuthentication(); // must be after UseRouting()
            app.UseAuthorization(); // must be after UseAuthentication()

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
