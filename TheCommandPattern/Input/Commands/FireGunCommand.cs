using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;
using TheCommandPattern.UI;

namespace TheCommandPattern.Input.Commands
{
    public class FireGunCommand : ICommand
    {
        public void Execute()
        {
            // Direct debug output instead of delegating to GamePadInput
            Debug.WriteLine("Gun fired!");
            
            // Display the message on screen
            MessageManager.Instance.DisplayMessage("Gun fired!");
            
            // Change background color when firing gun
            BackgroundManager.Instance.ChangeColor(BackgroundColor.Yellow);
        }

        public bool HandleInput(GamePadState currentState, GamePadState previousState)
        {
            // Check for Y button press (assuming FireGunCommand is mapped to Y)
            if (IsNewButtonPress(currentState.Buttons.Y, previousState.Buttons.Y))
            {
                Execute();
                return true;
            }
            return false;
        }

        public void ChangeBackgroundColor(Color color)
        {
            // Convert from Color to BackgroundColor enum
            BackgroundColor bgColor = BackgroundColor.Yellow; // Default to Yellow for FireGunCommand
            
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