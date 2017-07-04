using AutoMapper;
using EKE.Data;
using EKE.Data.Entities;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Entities.Identity;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Services.Admin;
using EKE_Gyopar.Web.ViewModels.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;

namespace EKE_Gyopar.Web
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

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<BaseDbContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("EKEConnectionString")));

            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            //Add Framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<UserSeed>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IMagazineService, MagazineService>();

            services.AddTransient<IEntityBaseRepository<Article>, EntityBaseRepository<Article>>();
            services.AddTransient<IEntityBaseRepository<Magazine>, EntityBaseRepository<Magazine>>();
            services.AddTransient<IEntityBaseRepository<MagazineCategory>, EntityBaseRepository<MagazineCategory>>();
            services.AddTransient<IEntityBaseRepository<Tag>, EntityBaseRepository<Tag>>();
            services.AddTransient<IEntityBaseRepository<MediaElement>, EntityBaseRepository<MediaElement>>();
            services.AddTransient<IEntityBaseRepository<Author>, EntityBaseRepository<Author>>();
            //Add Services
            services.AddMvc();
            services.AddSession();
            services.AddAutoMapper();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
