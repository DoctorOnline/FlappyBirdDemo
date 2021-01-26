using FlappyBirdDemo.Core.Interfaces;
using FlappyBirdDemo.Core.Models;
using System;

namespace FlappyBirdDemo.Core.Builders
{
    public sealed class PipeBuilder : IGenericBuilder<Pipe>
    {
        private readonly IGameConfiguration _config;
        private readonly Random _random = new();

        public PipeBuilder(IGameConfiguration config)
            => _config = config;

        public Pipe Build()
            => new(_config.Width, _random.Next(0, 100));
    }
}