using FuzbollLadder.Options;
using FuzbollLadder.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FuzbollLadder
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc();

            services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));
            services.Configure<IntegrationOptions>(Configuration.GetSection("Integration"));

            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IIntegrationService, SlackIntegrationService>();
            services.AddSingleton<IJobService, JobService>();
            services.AddSingleton<IRatingService, EloRatingService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Players}/{action=Index}/{id?}");
            });

            app.ApplicationServices.GetService<IJobService>().Initialize();
        }
    }
}