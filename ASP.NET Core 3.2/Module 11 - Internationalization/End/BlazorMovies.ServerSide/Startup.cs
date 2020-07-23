using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorMovies.ServerSide.Areas.Identity;
using BlazorMovies.Components.Helpers;
using BlazorMovies.Shared.Repositories;
using BlazorMovies.SharedBackend.Repositories;
using BlazorMovies.SharedBackend;
using BlazorMovies.SharedBackend.Helpers;
using BlazorMovies.ServerSide.Helpers;
using Blazor.FileReader;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace BlazorMovies.ServerSide
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
            services.AddSignalR().AddAzureSignalR(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("signalR");
                options.ServerStickyMode = Microsoft.Azure.SignalR.ServerStickyMode.Required;
            });
            services.AddTransient<IDisplayMessage, DisplayMessage>();
            services.AddScoped<IFileStorageService>(provider =>
            {
                var env = provider.GetService<IWebHostEnvironment>();
                var navManager = provider.GetService<NavigationManager>();
                return new InAppStorageService(env.WebRootPath, navManager.Uri);
            });

            services.AddLocalization();
            services.AddBlazorMovies();
            services.AddScoped<IAuthenticationStateService, AuthenticationStateServiceServerSide>();
            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                 new CultureInfo("en-US"),
                new CultureInfo("es-US"),
                new CultureInfo("es-DO"),
                new CultureInfo("fr-FR"),
                new CultureInfo("es"),
                new CultureInfo("en")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                DefaultRequestCulture = new RequestCulture("en-US")
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
