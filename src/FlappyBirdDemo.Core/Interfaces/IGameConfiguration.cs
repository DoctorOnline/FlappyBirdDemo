namespace FlappyBirdDemo.Core.Interfaces
{
    public interface IGameConfiguration
    {
        int Height { get; }
        int Width { get; }
        int Gravity { get; }
        int Delay { get; }
        int InitialSpeed { get; }
        int JumpStrength { get; }
    }
}