using FlappyBirdDemo.Core.Interfaces;
using FlappyBirdDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBirdDemo.Core
{
    public sealed class GameManager : IGameManager
    {
        private readonly IGameConfiguration _config;
        private const int Width = 500;
        private const int Height = 580;
        private Pipe _prevPipe = null;

        public GameManager(IGameConfiguration config)
        {
            _config = config;
            Bird = Bird.Create(Height, _config.JumpStrength);
        }

        public event EventHandler StateChanged;

        public bool IsRunning { get; private set; }

        public Bird Bird { get; private set; }

        public ICollection<Pipe> Pipes { get; private set; }

        public int Score { get; private set; } = 0;
        
        public void StartGame()
        {
            if (IsRunning)
                return;

            Score = 0;
            Bird = Bird.Create(Height, 50);
            Pipes = new List<Pipe>();

            IsRunning = true;
            GameLoopAsync();
        }

        private async void GameLoopAsync()
        {
            while (IsRunning)
            {
                MoveObject();
                CheckForCollisions();
                ManagePipes();

                StateChanged?.Invoke(this, EventArgs.Empty);
                await Task.Delay(_config.Delay);
            }
        }

        private void GameOver() => IsRunning = false;

        private void MoveObject()
        {
            Bird.Fall(_config.Gravity);

            foreach (var pipe in Pipes)
            {
                pipe.Move(_config.InitialSpeed);

                if (pipe != _prevPipe && pipe.IsPassed())
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

            var centerPipe = Pipes.FirstOrDefault(p => p.IsCentered());

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
            if (!Pipes.Any() || Pipes.Last().PositionX <= Width / 2)
                Pipes.Add(Pipe.Create(Width));

            if (Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());

             Debug.Write(Pipes.Count);
        }
    }
}