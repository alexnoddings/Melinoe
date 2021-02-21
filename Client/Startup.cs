using System;
using System.Net.Http;
using Blazored.LocalStorage;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Fluxor;
using Melinoe.Client.Middleware;
using Melinoe.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Melinoe.Client
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebAssemblyHostEnvironment HostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebAssemblyHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(_ => new HttpClient {BaseAddress = new Uri(HostEnvironment.BaseAddress)});

            services.AddFluxor(options =>
            {
                options.ScanAssemblies(typeof(App).Assembly);
                options.UseRouting();

                if (HostEnvironment.IsDevelopment() || HostEnvironment.IsStaging())
                {
                    options.AddMiddleware<StateLoggerMiddleware>();
                    options.UseReduxDevTools();
                }
            });

            services.AddScoped<GameService>();

            services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;

                options.DelayTextOnKeyPress = true;
                options.DelayTextOnKeyPressInterval = 300;
            });
            services.AddBootstrapProviders();
            services.AddFontAwesomeIcons();

            services.AddBlazoredToast();
            services.AddBlazoredLocalStorage();
        }

        public void Configure(WebAssemblyHost host)
        {
            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();
        }
    }
}
