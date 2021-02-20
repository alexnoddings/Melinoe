using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace Melinoe.Client
{
    public static class Program
    {
        public static Task Main(string[] args) =>
            CreateHost(args).RunAsync();

        public static WebAssemblyHost CreateHost(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var startup = new Startup(builder.Configuration, builder.HostEnvironment);
            startup.ConfigureServices(builder.Services);

            WebAssemblyHost host = builder.Build();
            startup.Configure(host);

            return host;
        }

}
}
