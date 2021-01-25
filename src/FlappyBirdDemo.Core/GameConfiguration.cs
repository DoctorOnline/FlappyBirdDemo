using FlappyBirdDemo.Core.Interfaces;

namespace FlappyBirdDemo.Core
{
    public sealed class GameConfiguration : IGameConfiguration
    {
        public int Gravity { get; init; }
        public int Delay { get; init; }
        public int InitialSpeed { get; init; }
        public int JumpStrength { get; init; }
    }
}