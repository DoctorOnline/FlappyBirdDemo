namespace FlappyBirdDemo.Core
{
    public sealed class GameConfiguration
    {
        public int Height { get; } = 580;
        public int Width { get; } = 500;
        public int Gravity { get; init; }
        public int Delay { get; init; }
        public int InitialSpeed { get; init; }
        public int JumpStrength { get; init; }
    }
}