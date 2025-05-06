using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;

namespace TheCommandPattern.Input.Commands
{
    public class SwapWeaponCommand : ICommand
    {
        // Add reference to GamePadInput to resolve ambiguity
        private readonly GamePadInput _gamePadInput;

        // Add constructor to accept and store GamePadInput
        public SwapWeaponCommand(GamePadInput gamePadInput = null)
        {
            _gamePadInput = gamePadInput;
        }

        public void Execute()
        {
            // Use GamePadInput if available, otherwise use direct debug output
            if (_gamePadInput != null)
            {
                _gamePadInput.SwapWeapon();
            }
            else
            {
                Debug.WriteLine("Weapon swapped!");
            }
            
            // Change background color when swapping weapons
            BackgroundManager.Instance.ChangeColor(BackgroundColor.Red);
        }

        public bool HandleInput(GamePadState currentState, GamePadState previousState)
        {
            // Check for B button press (assuming SwapWeaponCommand is mapped to B)
            if (IsNewButtonPress(currentState.Buttons.B, previousState.Buttons.B))
            {
                Execute();
                return true;
            }
            return false;
        }

        public void ChangeBackgroundColor(Color color)
        {
            // Convert from Color to BackgroundColor enum
            BackgroundColor bgColor = BackgroundColor.Red; // Default to Red for SwapWeaponCommand
            
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