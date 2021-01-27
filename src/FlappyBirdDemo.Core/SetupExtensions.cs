using FlappyBirdDemo.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlappyBirdDemo.Core
{
    public static class SetupExtensions
    {
        public static IServiceCollection AddFlappyBirdDemo(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddSingleton<IGame, Game>()
                .AddSingleton<IGameObjectsFactory, GameObjectsFactory>()
                .Configure<GameConfiguration>(configuration.GetSection(nameof(GameConfiguration)));
    }
}