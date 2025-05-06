using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;

namespace TheCommandPattern.Input.Commands
{
    /// <summary>
    /// Special command implementation that directly changes the background color.
    /// Can be mapped to any gamepad button by specifying in the constructor.
    /// Implements the Command Pattern for a standalone background color change action.
    /// 
    /// Related files:
    /// - TheCommandPattern\Background\BackgroundManager.cs - provides color change functionality
    /// - TheCommandPattern\Background\Colors.cs - defines available colors
    /// </summary>
    public class ChangeBackgroundColorCommand : ICommand
    {
        /// <summary>
        /// The color to change to when this command is executed.
        /// Set in the constructor and used in Execute().
        /// See TheCommandPattern\Background\Colors.cs for color definitions.
        /// </summary>
        private readonly BackgroundColor _color;
        
        /// <summary>
        /// Which button this command is mapped to.
        /// Used in HandleInput() to check if this command should execute.
        /// </summary>
        private readonly Buttons _button;

        /// <summary>
        /// Constructor that specifies which color to change to and which button to map to.
        /// Can be used to create button-specific color change commands.
        /// 
        /// Example usage:
        /// var redButton = new ChangeBackgroundColorCommand(BackgroundColor.Red, Buttons.X);
        /// </summary>
        /// <param name="color">Background color to change to when executed</param>
        /// <param name="button">Gamepad button to map this command to</param>
        public ChangeBackgroundColorCommand(BackgroundColor color, Buttons button = Buttons.X)
        {
            _color = color;
            _button = button;
        }

        /// <summary>
        /// Executes the background color change when the specified button is pressed.
        /// Directly updates the BackgroundManager without going through GamePadInput.
        /// 
        /// Called from:
        /// - HandleInput method in this class when the specified button is pressed
        /// </summary>
        public void Execute()
        {
            // Direct debug output and background color change
            // See TheCommandPattern\Background\BackgroundManager.cs:ChangeColor method
            Debug.WriteLine($"Background color changed to {_color}");
            BackgroundManager.Instance.ChangeColor(_color);
        }

        /// <summary>
        /// Checks if the specified button was newly pressed and executes the command if so.
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
            // Check for the button press based on the assigned button
            ButtonState currentButtonState = GetButtonState(currentState, _button);
            ButtonState previousButtonState = GetButtonState(previousState, _button);
            
            if (IsNewButtonPress(currentButtonState, previousButtonState))
            {
                Execute();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes the background color of the game.
        /// Unlike other commands, this one is specifically designed for color changes.
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
            BackgroundColor bgColor = BackgroundColor.Blue; // Default fallback
            
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
        /// Helper method to get the state of a specific button from a GamePadState.
        /// Handles mapping from Buttons enum to actual button state.
        /// </summary>
        /// <param name="state">The gamepad state to check</param>
        /// <param name="button">Which button to check</param>
        /// <returns>The state of the specified button</returns>
        private ButtonState GetButtonState(GamePadState state, Buttons button)
        {
            switch (button)
            {
                case Buttons.X:
                    return state.Buttons.X;
                case Buttons.Y:
                    return state.Buttons.Y;
                case Buttons.A:
                    return state.Buttons.A;
                case Buttons.B:
                    return state.Buttons.B;
                default:
                    return ButtonState.Released;
            }
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