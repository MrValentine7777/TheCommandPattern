using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;

namespace TheCommandPattern.Input.Commands
{
    /// <summary>
    /// Command implementation that handles weapon swapping behavior.
    /// Maps to the B button on the gamepad.
    /// Implements the Command Pattern by encapsulating the weapon swap action.
    /// 
    /// Related files:
    /// - TheCommandPattern\Input\GamePadInput.cs - creates this command and provides SwapWeapon() method
    /// - TheCommandPattern\Input\InputHandler.cs - maps this command to the B button
    /// - TheCommandPattern\Background\BackgroundManager.cs - used for color changes
    /// </summary>
    public class SwapWeaponCommand : ICommand
    {
        /// <summary>
        /// Reference to the GamePadInput that owns this command.
        /// Used to call back to GamePadInput.SwapWeapon() when executed.
        /// Set in the constructor by TheCommandPattern\Input\GamePadInput.cs
        /// </summary>
        private readonly GamePadInput _gamePadInput;

        /// <summary>
        /// Constructor accepts a reference to the GamePadInput.
        /// This enables the command to call back to GamePadInput.SwapWeapon().
        /// 
        /// Called from:
        /// - TheCommandPattern\Input\GamePadInput.cs constructor
        /// </summary>
        /// <param name="gamePadInput">Reference to the GamePadInput instance</param>
        public SwapWeaponCommand(GamePadInput gamePadInput = null)
        {
            _gamePadInput = gamePadInput;
        }

        /// <summary>
        /// Executes the weapon swap action when the B button is pressed.
        /// Either calls back to GamePadInput.SwapWeapon() or directly outputs debug message.
        /// Also changes background color to red.
        /// 
        /// Called from:
        /// - HandleInput method in this class when B button is pressed
        /// </summary>
        public void Execute()
        {
            // Use GamePadInput if available, otherwise use direct debug output
            // This calls back to TheCommandPattern\Input\GamePadInput.cs:SwapWeapon method
            if (_gamePadInput != null)
            {
                _gamePadInput.SwapWeapon();
            }
            else
            {
                Debug.WriteLine("Weapon swapped!");
            }
            
            // Change background color when swapping weapons
            // See TheCommandPattern\Background\BackgroundManager.cs:ChangeColor method
            BackgroundManager.Instance.ChangeColor(BackgroundColor.Red);
        }

        /// <summary>
        /// Checks if the B button was newly pressed and executes the command if so.
        /// Called by InputHandler.HandleInput() for each frame.
        /// 
        /// Called from:
        /// - TheCommandPattern\Input\InputHandler.cs:HandleInput method
        /// </summary>
        /// <param name="currentState">Current frame's gamepad state</param>
        /// <param name="previousState">Previous frame's gamepad state</param>
        /// <returns>True if the command was executed, otherwise false</returns>
        public bool HandleInput(GamePadState currentState, GamePadState previousState)
        {
            // Check for B button press (SwapWeaponCommand is mapped to B in GamePadInput.cs)
            if (IsNewButtonPress(currentState.Buttons.B, previousState.Buttons.B))
            {
                Execute();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes the background color of the game.
        /// For SwapWeaponCommand, typically changes to Red.
        /// 
        /// Related to:
        /// - TheCommandPattern\Background\BackgroundManager.cs
        /// - TheCommandPattern\Background\Colors.cs
        /// </summary>
        /// <param name="color">The color to change to</param>
        public void ChangeBackgroundColor(Color color)
        {
            // Convert from Color to BackgroundColor enum
            // See TheCommandPattern\Background\Colors.cs for the enum definition
            BackgroundColor bgColor = BackgroundColor.Red; // Default to Red for SwapWeaponCommand
            
            if (color == Color.Red)
                bgColor = BackgroundColor.Red;
            else if (color == Color.Green)
                bgColor = BackgroundColor.Green;
            else if (color == Color.Blue)
                bgColor = BackgroundColor.Blue;
            else if (color == Color.Yellow)
                bgColor = BackgroundColor.Yellow;
            
            // Apply the color change through the BackgroundManager singleton
            // See TheCommandPattern\Background\BackgroundManager.cs:ChangeColor method
            Debug.WriteLine($"Background color changed to {bgColor}");
            BackgroundManager.Instance.ChangeColor(bgColor);
        }

        /// <summary>
        /// Helper method to detect when a button is newly pressed.
        /// Compares current and previous states to detect the press transition.
        /// </summary>
        /// <param name="currentState">Current button state</param>
        /// <param name="previousState">Previous button state</param>
        /// <returns>True if button was just pressed, otherwise false</returns>
        private bool IsNewButtonPress(ButtonState currentState, ButtonState previousState)
        {
            return currentState == ButtonState.Pressed && previousState == ButtonState.Released;
        }
    }
}