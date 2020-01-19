using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VMRent.Managers;
using VMRent.Models;
using VMRent.Repositories;
using VMRent.Stores;

namespace VMRent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            # region Repository pattern DI
            services.AddSingleton<IUserRepository, MemoryUserRepository>();
            services.AddSingleton<IRoleRepository, MemoryRoleRepository>();
            services.AddSingleton<IUserRoleRepository, MemoryUserRoleRepository>();
            services.AddSingleton<IVmRepository, MemoryVmRepository>();
            services.AddSingleton<IUserVmRepository, MemoryUserVmRepository>();
            # endregion
            
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services.AddTransient<SignInManager<User>, SignInManager>();
            services.AddTransient<UserManager<User>>();
            services.AddTransient<VmManager>();
            services.AddTransient<ReservationManager>();

            services.AddIdentity<User, Role>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddDefaultTokenProviders();

            services.AddAuthentication();
            services.AddAuthorization();
            
            services.AddMvc(options => { options.EnableEndpointRouting = false; });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
                {
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );
        }
    }
}