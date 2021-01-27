using FlappyBirdDemo.Core.Models;

namespace FlappyBirdDemo.Core.Interfaces
{
    public interface IGameObjectsFactory
    {
        Bird CreateBird();
        Pipe CreatePipe();
    }
}