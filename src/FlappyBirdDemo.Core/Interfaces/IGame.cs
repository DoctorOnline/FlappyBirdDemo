using FlappyBirdDemo.Core.Models;
using System;
using System.Collections.Generic;

namespace FlappyBirdDemo.Core.Interfaces
{
    public interface IGame
    {
        event EventHandler StateChanged;

        int Score { get; }

        bool IsRunning { get; }

        Bird Bird { get; }

        ICollection<Pipe> Pipes { get; }

        void StartGame();

        void Jump();
    }
}