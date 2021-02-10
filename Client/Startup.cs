﻿using System;
using System.Net.Http;
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
        }
    }
}
