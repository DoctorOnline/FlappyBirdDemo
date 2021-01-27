using FlappyBirdDemo.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlappyBirdDemo.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            ConfigureServices(builder.Services, builder.Configuration);
            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
            => services.AddFlappyBirdDemo(configuration);
    }
}