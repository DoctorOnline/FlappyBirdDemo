using FlappyBirdDemo.Core.Interfaces;
using FlappyBirdDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace FlappyBirdDemo.Core
{
    public sealed class Game : IGame
    {
        private readonly GameConfiguration _config;
        private readonly IGameObjectsFactory _factory;
        private readonly int _centerX;
        private Pipe _prevPipe = null;

        public Game(IOptions<GameConfiguration> config, IGameObjectsFactory factory)
        {
            _config = config.Value;
            _factory = factory;
            _centerX = _config.Width / 2;
            Bird = _factory.CreateBird();
        }

        public event EventHandler StateChanged;

        public bool IsRunning { get; private set; }

        public Bird Bird { get; private set; }

        public ICollection<Pipe> Pipes { get; } = new List<Pipe>();

        public int Score { get; private set; } = 0;
        
        public void StartGame()
        {
            if (IsRunning)
                return;

            Score = 0;
            Bird = _factory.CreateBird();
            Pipes.Clear();

            IsRunning = true;
            GameLoopAsync();
        }

        public void Jump()
            => Bird.Jump(_config.JumpStrength, b => b.PositionY < _config.Height - b.Height);

        private async void GameLoopAsync()
        {
            while (IsRunning)
            {
                MoveObjects();
                CheckForCollisions();
                ManagePipes();

                StateChanged?.Invoke(this, EventArgs.Empty);
                await Task.Delay(_config.Delay);
            }
        }

        private void MoveObjects()
        {
            Bird.Fall(_config.Gravity);

            foreach (var pipe in Pipes)
            {
                pipe.Move(_config.InitialSpeed);

                if (pipe != _prevPipe && pipe.HasPassedCenter(_centerX))
                {
                    Score++;
                    _prevPipe = pipe;
                }
            }
        }

        private void CheckForCollisions()
        {
            if (Bird.IsOnGround())
                GameOver();

            var centerPipe = Pipes.FirstOrDefault(p => p.IsCentered(_centerX));

            if (centerPipe is not null)
            {
                const int groundHeight = 150;
                var hasCollidedWithBottom = Bird.PositionY < centerPipe.GapBottom - groundHeight;
                var hasCollidedWithTop = Bird.PositionY + Bird.Height > centerPipe.GapTop - groundHeight;

                if (hasCollidedWithTop || hasCollidedWithBottom)
                    GameOver();
            }
        }

        private void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().HasPassedCenter(_centerX))
                Pipes.Add(_factory.CreatePipe());

            if (Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());
        }

        private void GameOver() => IsRunning = false;
    }
}