namespace FlappyBirdDemo.Core.Models
{
    public sealed class Pipe
    {
        internal Pipe(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public int Height { get; } = 300;
        public int Width { get; } = 60;
        public int Gap { get; } = 130;
        public int PositionX { get; internal set; }
        public int PositionY { get; internal set; }

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

        public bool HasEnteredCenter(int center)
            => PositionX <= center + (Width / 2);
    }
}
