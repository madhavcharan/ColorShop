using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ColorShop.Controllers;
using ColorShop.Data;
using ColorShop.Data.Entities;
using ColorShop.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ColorShop
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ColorShopContext>();
            services.AddDbContext<ColorShopContext>(cfg =>
            { 
                cfg.UseSqlServer(config.GetConnectionString("ColorConnectionString"));
            });
        
            services.AddTransient<ColorShopSeeder, ColorShopSeeder>();
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IPaintingRepository, PaintingRepository>();
            services.AddControllersWithViews();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if(env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
       
            app.UseNodeModules();
            
            app.UseEndpoints(cfg =>
           {
               cfg.MapControllerRoute("Fallback",
                   "{controller}/{action}/{id?}",
                   new { controller = "App", action = "Index" });

           });
        }
    }
}
