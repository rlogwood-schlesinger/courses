using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_UnderTheHood
{
    using core_ident_1;

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
            services.AddAuthentication(MyGlobals.AuthSchemeName)
                .AddCookie(MyGlobals.AuthSchemeName, options =>
            {
                options.Cookie.Name = MyGlobals.AuthSchemeName;
                options.LoginPath = "/Account/Login";  // this is the default
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(MyGlobals.AdminOnly,
                    policy => policy.RequireClaim("Admin"));

                options.AddPolicy(MyGlobals.PolicyHRDepartment,
                    policy => policy.RequireClaim("Department", "HR"));

                options.AddPolicy(MyGlobals.PolicyHRManager, policy => policy
                    .RequireClaim("Department", "HR")
                    .RequireClaim("Manager"));                    
                    
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}