using System;
using FlappyBirdDemo.Core.Interfaces;
using FlappyBirdDemo.Core.Models;
using Microsoft.Extensions.Options;

namespace FlappyBirdDemo.Core
{
    public sealed class GameObjectsFactory : IGameObjectsFactory
    {
        private readonly GameConfiguration _config;
        private readonly Random _random = new();

        public GameObjectsFactory(IOptions<GameConfiguration> config)
            => _config = config.Value;

        public Bird CreateBird()
            => new(220, _config.Height / 2);

        public Pipe CreatePipe()
            => new(_config.Width, _random.Next(0, 100));
    }
}