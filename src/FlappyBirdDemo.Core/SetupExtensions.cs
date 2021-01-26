using FlappyBirdDemo.Core.Builders;
using FlappyBirdDemo.Core.Interfaces;
using FlappyBirdDemo.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlappyBirdDemo.Core
{
    public static class SetupExtensions
    {
        public static IServiceCollection AddFlappyBirdDemo(this IServiceCollection services)
        {
            return services
                .AddSingleton<IGame, Game>()
                .AddSingleton<IGenericBuilder<Pipe>, PipeBuilder>()
                .AddSingleton<IGenericBuilder<Bird>, BirdBuilder>()
                .AddSingleton<IGameConfiguration>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return config?.GetSection(nameof(GameConfiguration)).Get<GameConfiguration>();
            });
        }
    }
}