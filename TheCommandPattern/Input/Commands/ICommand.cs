using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheCommandPattern.Input.Commands
{
    /// <summary>
    /// Command interface that defines the contract for all game commands.
    /// This is a core part of the Command Pattern implementation:
    /// - Defines a common interface for all game actions
    /// - Enables button-to-action mapping without tight coupling
    /// - Allows for easy extension with new commands
    /// 
    /// Related files:
    /// - TheCommandPattern\Input\InputHandler.cs - stores command objects and handles input
    // C# equivalent of the C++ Command abstract class
    public interface ICommand
    {
        void Execute();
        bool HandleInput(GamePadState currentState, GamePadState previousState);
        void ChangeBackgroundColor(Color color);
    }
}