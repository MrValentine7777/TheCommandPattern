using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;

namespace TheCommandPattern.Input.Commands
{
    public class JumpCommand : ICommand
    {
        // Add reference to GamePadInput to resolve ambiguity
        private readonly GamePadInput _gamePadInput;

        // Add constructor to accept and store GamePadInput
        public JumpCommand(GamePadInput gamePadInput = null)
        {
            _gamePadInput = gamePadInput;
        }

        public void Execute()
        {
            // Use GamePadInput if available, otherwise use direct debug output
            if (_gamePadInput != null)
            {
                _gamePadInput.Jump();
            }
            else
            {
                Debug.WriteLine("Character jumped!");
            }
            
            // Change background color when jumping
            BackgroundManager.Instance.ChangeColor(BackgroundColor.Blue);
        }

        public bool HandleInput(GamePadState currentState, GamePadState previousState)
        {
            // Check for X button press (assuming JumpCommand is mapped to X)
            if (IsNewButtonPress(currentState.Buttons.X, previousState.Buttons.X))
            {
                Execute();
                return true;
            }
            return false;
        }

        public void HandleButtonInput(GamePadState currentState, GamePadState previousState)
        {
            // Implementation of required interface method
            HandleInput(currentState, previousState);
        }

        public void ChangeBackgroundColor(Color color)
        {
            // Convert from Color to BackgroundColor enum
            BackgroundColor bgColor = BackgroundColor.Blue; // Default to Blue for JumpCommand
            
            if (color == Color.Red)
                bgColor = BackgroundColor.Red;
            else if (color == Color.Green)
                bgColor = BackgroundColor.Green;
            else if (color == Color.Blue)
                bgColor = BackgroundColor.Blue;
            else if (color == Color.Yellow)
                bgColor = BackgroundColor.Yellow;
            
            // Apply the color change
            Debug.WriteLine($"Background color changed to {bgColor}");
            BackgroundManager.Instance.ChangeColor(bgColor);
        }

        private bool IsNewButtonPress(ButtonState currentState, ButtonState previousState)
        {
            return currentState == ButtonState.Pressed && previousState == ButtonState.Released;
        }
    }
}