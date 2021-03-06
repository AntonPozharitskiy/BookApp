﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using API.Mapping;
using API.Validators;
using Autofac;
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
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AutomapperConfig.Configure();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(validator =>
                {
                    validator.RegisterValidatorsFromAssemblyContaining<RegisterUserValidator>();
                    validator.RegisterValidatorsFromAssemblyContaining<AuthenticateUserValidator>();
                    validator.RegisterValidatorsFromAssemblyContaining<BookValidator>();
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:BookApp"]);
       
            },ServiceLifetime.Scoped);
            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(x => x.GetRequiredService<ApplicationContext>().Books);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,

                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });


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

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<BLLModules>();
            builder.RegisterModule<DALModules>();
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
             app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseDefaultFiles();
app.UseStaticFiles();
            app.UseMvc(routes => { routes.MapRoute("default", "controller/action/{id}"); });
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {  
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
