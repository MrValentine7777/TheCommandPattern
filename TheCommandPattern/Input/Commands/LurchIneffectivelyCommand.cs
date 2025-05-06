using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;

namespace TheCommandPattern.Input.Commands
{
    public class LurchIneffectivelyCommand : ICommand
    {
        // Add reference to GamePadInput to resolve ambiguity
        private readonly GamePadInput _gamePadInput;

        // Add constructor to accept and store GamePadInput
        public LurchIneffectivelyCommand(GamePadInput gamePadInput = null)
        {
            _gamePadInput = gamePadInput;
        }

        public void Execute()
        {
            // Use GamePadInput if available, otherwise use direct debug output
            if (_gamePadInput != null)
            {
                _gamePadInput.LurchIneffectively();
            }
            else
            {
                Debug.WriteLine("Character lurched... ineffectively!");
            }
            
            // Change background color when lurching ineffectively
            BackgroundManager.Instance.ChangeColor(BackgroundColor.Green);
        }

        public bool HandleInput(GamePadState currentState, GamePadState previousState)
        {
            // Check for A button press (assuming LurchIneffectivelyCommand is mapped to A)
            if (IsNewButtonPress(currentState.Buttons.A, previousState.Buttons.A))
            {
                Execute();
                return true;
            }
            return false;
        }

        public void ChangeBackgroundColor(Color color)
        {
            // Convert from Color to BackgroundColor enum
            BackgroundColor bgColor = BackgroundColor.Green; // Default to Green for LurchIneffectivelyCommand
            
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