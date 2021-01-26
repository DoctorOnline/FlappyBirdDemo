using FlappyBirdDemo.Core.Interfaces;
using FlappyBirdDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBirdDemo.Core
{
    public sealed class Game : IGame
    {
        private readonly IGameConfiguration _config;
        private readonly IGenericBuilder<Pipe> _pipeBuilder;
        private readonly IGenericBuilder<Bird> _birdBuilder;
        private readonly int _centerX;
        private Pipe _prevPipe = null;

        public Game(IGameConfiguration config, IGenericBuilder<Pipe> pipeBuilder, IGenericBuilder<Bird> birdBuilder)
        {
            _config = config;
            _pipeBuilder = pipeBuilder;
            _birdBuilder = birdBuilder;
            _centerX = _config.Width / 2;
            Bird = _birdBuilder.Build();
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
            Bird = _birdBuilder.Build();
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
                Pipes.Add(_pipeBuilder.Build());

            if (Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());
        }

        private void GameOver() => IsRunning = false;
    }
}