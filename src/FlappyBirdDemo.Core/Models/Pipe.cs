using System;

namespace FlappyBirdDemo.Core.Models
{
    public sealed class Pipe
    {
        private static readonly Random Random = new();

        public Pipe(int positionX)
            => PositionX = positionX;

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

        public bool IsCentered(int center)
            => HasEnteredCenter(center) && !HasPassedCenter(center);

        public bool HasPassedCenter(int center)
            => PositionX <= center - (Width / 2) - Width;

        private bool HasEnteredCenter(int center)
            => PositionX <= center + (Width / 2);
    }
}
