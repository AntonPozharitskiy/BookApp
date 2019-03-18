using System;
using System.Threading.Tasks;
using API.Controllers;
using BLL;
using BLL.Config;
using BLL.DataAccess;
using BLL.Entities;
using BLL.Finders;
using BLL.Managers;
using BLL.Services;
using BLL.Wrappers;
using DAL;
using DAL.Context;
using DAL.Finder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:BookApp"]);
            });
            services.AddIdentity<User, Role>(options =>
                {
                    //настроить валидационные настройки!
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddTransient(x => x.GetRequiredService<ApplicationContext>().Books);
            
            services.AddTransient<ISignInManager, SignInManagerWrapper>();
            services.AddTransient<IUserManager, UserManagerWrapper>();
            services.AddTransient<IRoleManager, RoleManagerWrapper>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IBookService, BookService>();
            services.AddScoped<IRepository<Book>, Repository<Book>>();
            services.AddTransient<IBookFinder, BookFinder>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<JwtConfigurationSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext context, IServiceProvider serviceProvider)
        {
            app.Use(async (ctx, next) =>
            {
                
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseMvc(routes => { routes.MapRoute("default", "controller/action/{id}"); });

            app.UseAuthentication();
            CreateRoles(serviceProvider).Wait();

        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            
            string[] roleNames = { "Admin", "User" };
            
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (roleExist) continue;
                var roleToAdd = new Role {Name = roleName};
                
                await roleManager.CreateAsync(roleToAdd);
            }

        }
    }
}
