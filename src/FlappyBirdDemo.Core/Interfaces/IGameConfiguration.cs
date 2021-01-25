namespace FlappyBirdDemo.Core.Interfaces
{
    public interface IGameConfiguration
    {
        int Gravity { get; }
        int Delay { get; }
        int InitialSpeed { get; }
        int JumpStrength { get; }
    }
}