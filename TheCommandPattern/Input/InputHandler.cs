using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Input.Commands;

namespace TheCommandPattern.Input
{
    /// <summary>
    /// Core input handling class that implements the Command Pattern.
    /// Maps gamepad buttons to command objects that encapsulate game actions.
    /// 
    /// Related files:
    /// - TheCommandPattern\Input\GamePadInput.cs - creates and configures this handler
    /// - TheCommandPattern\Input\Commands\ICommand.cs - interface implemented by all commands
    /// - TheCommandPattern\Game1.cs - initiates the input handling process
    /// </summary>
    public class InputHandler
    {
        // Command objects for each gamepad button
        // These are concrete implementations of ICommand that will execute
        // specific actions when their respective buttons are pressed
        // See TheCommandPattern\Input\Commands\ICommand.cs
        private ICommand _buttonX; // Typically mapped to JumpCommand in GamePadInput.cs
        private ICommand _buttonY; // Typically mapped to FireGunCommand in GamePadInput.cs
        private ICommand _buttonA; // Typically mapped to LurchIneffectivelyCommand in GamePadInput.cs
        private ICommand _buttonB; // Typically mapped to SwapWeaponCommand in GamePadInput.cs
        
        // Stored to detect button press/release transitions between frames
        // Used by command objects to determine when a button is newly pressed
        private GamePadState _previousState;

        /// <summary>
        /// Constructor that accepts command objects for each gamepad button.
        /// Called by GamePadInput to configure the input mapping.
        /// 
        /// See TheCommandPattern\Input\GamePadInput.cs constructor for the
        /// specific command implementations used.
        /// </summary>
        /// <param name="buttonX">Command to execute when X is pressed (typically JumpCommand)</param>
        /// <param name="buttonY">Command to execute when Y is pressed (typically FireGunCommand)</param>
        /// <param name="buttonA">Command to execute when A is pressed (typically LurchIneffectivelyCommand)</param>
        /// <param name="buttonB">Command to execute when B is pressed (typically SwapWeaponCommand)</param>
        public InputHandler(
            ICommand buttonX, 
            ICommand buttonY, 
            ICommand buttonA, 
            ICommand buttonB)
        {
            _buttonX = buttonX;
            _buttonY = buttonY;
            _buttonA = buttonA;
            _buttonB = buttonB;
        }

        /// <summary>
        /// Processes input for the current frame and delegates to command objects.
        /// Called from GamePadInput.HandleInput() which is called by Game1.Update().
        /// 
        /// Each command's HandleInput method determines if its button was pressed
        /// and executes the appropriate action if needed.
        /// 
        /// See:
        /// - TheCommandPattern\Game1.cs:Update method
        /// - TheCommandPattern\Input\GamePadInput.cs:HandleInput method
        /// - Command classes in TheCommandPattern\Input\Commands\ directory
        /// </summary>
        /// <param name="gameTime">Timing information for the current frame</param>
        public void HandleInput(GameTime gameTime)
        {
            // Get the current state of the gamepad
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Each command decides if its button was pressed and executes if needed
            // For example, JumpCommand checks if X was pressed, FireGunCommand checks Y, etc.
            // See the HandleInput methods in each command implementation
            _buttonX.HandleInput(currentState, _previousState);
            _buttonY.HandleInput(currentState, _previousState);
            _buttonB.HandleInput(currentState, _previousState);
            _buttonA.HandleInput(currentState, _previousState);

            // Save the current state for next frame comparison
            // This allows commands to detect new button presses vs. held buttons
            _previousState = currentState;
        }
    }
}