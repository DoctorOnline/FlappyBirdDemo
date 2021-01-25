using System;

namespace FlappyBirdDemo.Core.Models
{
    public sealed class Pipe
    {
        private static readonly Random Random = new();

        public static Pipe Create(int worldWidth) 
            => new(worldWidth) {
                PositionX = worldWidth
            };

        private Pipe(int worldWidth)
        {
            _worldWidth = worldWidth;
            PositionX = worldWidth;
        }

        private readonly int _worldWidth;
        public int Height { get; } = 300;
        public int Width { get; } = 60;
        public int Gap { get; } = 130;
        public int PositionX { get; private set; }
        public int PositionY { get; } = Random.Next(0, 100);

        public int GapBottom => PositionY + Height;
        public int GapTop => GapBottom + Gap;

        public void Move(int speed) 
            => PositionX -= speed;

        public bool IsOffScreen()
            => PositionX <= Width * -1;

        public bool IsCentered()
            => HasEnteredCenter() && !HasExitedCenter();

        public bool IsPassed()
            => HasExitedCenter();

        private bool HasEnteredCenter()
            => PositionX <= (_worldWidth / 2) + (Width / 2);

        private bool HasExitedCenter()
            => PositionX <= (_worldWidth / 2) - (Width / 2) - Width;
    }
}
