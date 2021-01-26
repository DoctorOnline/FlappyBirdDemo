using System;

namespace FlappyBirdDemo.Core.Models
{
    public sealed class Bird
    {
        internal Bird(int positionY)
            => PositionY = positionY - Height;

        public int Height { get; } = 45;
        public int Width { get; } = 60;
        public int PositionY { get; private set; }

        public void Fall(int gravity) 
            => PositionY -= Math.Min(gravity, PositionY);

        public bool IsOnGround()
            => PositionY <= 0;

        public void Jump(int strength, Func<Bird, bool> canJump)
        {
            if (canJump(this))
                PositionY += strength;
        }
    }
}