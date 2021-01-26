using FlappyBirdDemo.Core.Interfaces;
using FlappyBirdDemo.Core.Models;

namespace FlappyBirdDemo.Core.Builders
{
    public sealed class BirdBuilder : IGenericBuilder<Bird>
    {
        private readonly IGameConfiguration _config;

        public BirdBuilder(IGameConfiguration config)
            => _config = config;

        public Bird Build()
            => new(_config.Height / 2);
    }
}