using System;

namespace FlappyBirdDemo.Core.Models
{
    public sealed class Bird
    {
        public static Bird Create(int worldHeight, int jumpStrength)
        {
            return new(worldHeight)
            {
                JumpStrength = jumpStrength
            };
        }

        private Bird(int worldHeight)
        {
            _worldHeight = worldHeight;
            PositionY = worldHeight / 2 - Height;
        }

        private readonly int _worldHeight;
        public int Height { get; } = 45;
        public int Width { get; } = 60;
        public int PositionY { get; private set; }
        private int JumpStrength { get; init; }

        public void Fall(int gravity) 
            => PositionY -= Math.Min(gravity, PositionY);

        public bool IsOnGround()
            => PositionY <= 0;

        public void Jump()
        {
            if (PositionY < _worldHeight - Height)
                PositionY += JumpStrength;
        }
    }
}