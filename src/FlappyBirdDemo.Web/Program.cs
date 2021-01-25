using FlappyBirdDemo.Core;
using FlappyBirdDemo.Core.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FlappyBirdDemo.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddSingleton<IGameManager, GameManager>();
            services.AddSingleton<IGameConfiguration>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return config?.GetSection(nameof(GameConfiguration)).Get<GameConfiguration>();
            });
        }
    }
}
