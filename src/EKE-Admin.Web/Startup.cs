using AutoMapper;
using EKE.Data;
using EKE.Data.Entities;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Entities.Identity;
using EKE.Data.Entities.Museum;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Services;
using EKE.Service.Services.Admin;
using EKE.Service.Services.Admin.Muzeum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NonFactors.Mvc.Grid;
using System;
using EKE.Data.Repository.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EKE_Admin.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        private static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<BaseDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EKEConnectionString")));

            RegisterServices(services);
            SetupServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            //Add Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<BaseDbContext>()
                .AddDefaultTokenProviders();
            //Add Framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddTransient<UserSeed>();
            services.AddTransient<IMagazineService, MagazineService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IMagazineCategoryService, MagazineCategoryService>();
            services.AddTransient<IGeneralService, GeneralService>();
            services.AddTransient<IMuseumService, MuseumService>();

            services.AddTransient<IEntityBaseRepository<Magazine>, EntityBaseRepository<Magazine>>();
            services.AddTransient<IEntityBaseRepository<Article>, EntityBaseRepository<Article>>();
            services.AddTransient<IEntityBaseRepository<Order>, EntityBaseRepository<Order>>();
            services.AddTransient<IEntityBaseRepository<MagazineCategory>, EntityBaseRepository<MagazineCategory>>();
            services.AddTransient<IEntityBaseRepository<Tag>, EntityBaseRepository<Tag>>();
            services.AddTransient<IEntityBaseRepository<MediaElement>, EntityBaseRepository<MediaElement>>();
            services.AddTransient<IEntityBaseRepository<Author>, EntityBaseRepository<Author>>();
            services.AddTransient<IEntityBaseRepository<Synonym>, EntityBaseRepository<Synonym>>();
            services.AddTransient<IEntityBaseRepository<ElementCategory>, EntityBaseRepository<ElementCategory>>();
            services.AddTransient<IEntityBaseRepository<Element>, EntityBaseRepository<Element>>();
            services.AddTransient<IEntityBaseRepository<ElementTag>, EntityBaseRepository<ElementTag>>();

            //Add Services
            services.AddMvc();
            services.AddSession();
            services.AddMvcGrid();
            services.AddAutoMapper();
        }

        private static void SetupServices(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, UserSeed seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            seeder.SeedAdminUser();
        }
    }
}
