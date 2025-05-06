using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;
using TheCommandPattern.UI;

namespace TheCommandPattern.Input.Commands
{
    /// <summary>
    /// Command implementation that handles firing a gun.
    /// Maps to the Y button on the gamepad.
    /// Implements the Command Pattern by encapsulating the fire gun action.
    /// 
    /// Unlike other commands, this one doesn't use a callback to GamePadInput,
    /// instead handling its logic directly within the command.
    /// 
    /// Related files:
    /// - TheCommandPattern\Input\GamePadInput.cs - creates this command
    /// - TheCommandPattern\Input\InputHandler.cs - maps this command to the Y button
    /// - TheCommandPattern\UI\MessageManager.cs - displays the "Gun fired!" message
    /// - TheCommandPattern\Background\BackgroundManager.cs - changes color when fired
    /// </summary>
    public class FireGunCommand : ICommand
    {
        /// <summary>
        /// Executes the fire gun action when the Y button is pressed.
        /// Outputs debug message, displays on-screen message, and changes background color.
        /// 
        /// Called from:
        /// - HandleInput method in this class when Y button is pressed
        /// </summary>
        public void Execute()
        {
            // Direct debug output instead of delegating to GamePadInput
            Debug.WriteLine("Gun fired!");
            
            // Display the message on screen using the MessageManager singleton
            // See TheCommandPattern\UI\MessageManager.cs:DisplayMessage method
            MessageManager.Instance.DisplayMessage("Gun fired!");
            
            // Change background color when firing gun
            // See TheCommandPattern\Background\BackgroundManager.cs:ChangeColor method
            BackgroundManager.Instance.ChangeColor(BackgroundColor.Yellow);
        }

        /// <summary>
        /// Checks if the Y button was newly pressed and executes the command if so.
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
            // Check for Y button press (FireGunCommand is mapped to Y in GamePadInput.cs)
            if (IsNewButtonPress(currentState.Buttons.Y, previousState.Buttons.Y))
            {
                Execute();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes the background color of the game.
        /// For FireGunCommand, typically changes to Yellow.
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
            BackgroundColor bgColor = BackgroundColor.Yellow; // Default to Yellow for FireGunCommand
            
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