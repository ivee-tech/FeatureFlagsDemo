using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using TestFrontEnd.Interfaces;
using TestFrontEnd.Features;
using Microsoft.CommonLib;
using Microsoft.ConfigReaders;

namespace TestFrontEnd
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpClient();

            IConfigReader appConfig = new NetCoreSettingsConfigReader(Configuration);
            // IConfigReader credKVConfig1 = new CredentialsKeyVaultConfigReader(appConfig);
            IConfigReader envConfig = new EnvVarsConfigReader(EnvironmentVariableTarget.Process);
            // IConfigReader credKVConfig2 = new CredentialsKeyVaultConfigReader(envConfig);

            services
                // .AddSingleton<IConfigReader>(provider => appConfig)
                .AddSingleton<IConfigReader>(provider => envConfig)
                // .AddSingleton<IConfigReader>(provider => credKVConfig1)
                // .AddSingleton<IConfigReader>(provider => credKVConfig2)
                .AddScoped<INamesClient, Names>()
                .AddScoped<IFeatureFlags, FeatureFlags>()
                .AddScoped<IGiphyClient, Giphy>()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
