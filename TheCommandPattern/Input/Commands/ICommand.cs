using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheCommandPattern.Input.Commands
{
    // C# equivalent of the C++ Command abstract class
    public interface ICommand
    {
        void Execute();
        bool HandleInput(GamePadState currentState, GamePadState previousState);
        void HandleButtonInput(GamePadState currentState, GamePadState previousState);
        void ChangeBackgroundColor(Color color);
    }
}