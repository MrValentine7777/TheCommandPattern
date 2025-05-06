// TheCommandPattern\Input\GamePadInput.cs
using System.Diagnostics;
using Microsoft.Xna.Framework;
using TheCommandPattern.Background;
using TheCommandPattern.Input.Commands;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.UI;

namespace TheCommandPattern.Input
{
    /// <summary>
    /// Manages gamepad input and creates connections between button presses and game actions.
    /// This class is a key component in the Command Pattern implementation:
    /// 1. It creates concrete command objects (JumpCommand, FireGunCommand, etc.)
    /// 2. It passes these commands to the InputHandler which maps them to specific buttons
    /// 3. It provides action methods that command objects can call when executed
    /// 
    /// Related files:
    /// - TheCommandPattern\Game1.cs - creates the GamePadInput instance
    /// - TheCommandPattern\Input\InputHandler.cs - handles the button-to-command mapping
    /// - TheCommandPattern\Input\Commands\JumpCommand.cs - command that calls Jump()
    /// - TheCommandPattern\Input\Commands\SwapWeaponCommand.cs - command that calls SwapWeapon()
    /// - TheCommandPattern\Input\Commands\LurchIneffectivelyCommand.cs - command that calls LurchIneffectively()
    /// - TheCommandPattern\UI\MessageManager.cs - displays messages triggered by commands
    /// </summary>
    public class GamePadInput
    {
        /// <summary>
        /// Reference to the InputHandler that maps buttons to commands.
        /// See TheCommandPattern\Input\InputHandler.cs for implementation.
        /// </summary>
        private InputHandler _inputHandler;

        /// <summary>
        /// Constructor creates command objects and configures the InputHandler.
        /// This is where the button-to-command mapping is established.
        /// 
        /// Called from:
        /// - TheCommandPattern\Game1.cs constructor
        /// </summary>
        public GamePadInput()
        {
            // Create command instances with reference to this GamePadInput instance
            // Commands that need to call back to GamePadInput methods receive 'this' reference
            // See TheCommandPattern\Input\Commands\JumpCommand.cs constructor
            var jumpCommand = new JumpCommand(this);
            
            // FireGunCommand doesn't need a reference back to GamePadInput since it handles
            // its own logic and directly uses MessageManager
            // See TheCommandPattern\Input\Commands\FireGunCommand.cs
            var fireGunCommand = new FireGunCommand();
            
            // These commands need references to call back to GamePadInput methods
            // See TheCommandPattern\Input\Commands\SwapWeaponCommand.cs
            // See TheCommandPattern\Input\Commands\LurchIneffectivelyCommand.cs
            var swapWeaponCommand = new SwapWeaponCommand(this);
            var lurchCommand = new LurchIneffectivelyCommand(this);

            // Initialize the InputHandler with concrete command implementations
            // This maps specific commands to gamepad buttons
            // See TheCommandPattern\Input\InputHandler.cs constructor
            _inputHandler = new InputHandler(
                jumpCommand,         // X button - See JumpCommand.HandleInput()
                fireGunCommand,      // Y button - See FireGunCommand.HandleInput()
                lurchCommand,        // A button - See LurchIneffectivelyCommand.HandleInput()
                swapWeaponCommand    // B button - See SwapWeaponCommand.HandleInput()
            );
        }

        /// <summary>
        /// Processes input for the current frame using the Command pattern.
        /// Delegates to InputHandler which checks button states and executes the appropriate commands.
        /// 
        /// Called from:
        /// - TheCommandPattern\Game1.cs:Update method each frame
        /// </summary>
        /// <param name="gameTime">Timing values for the current frame</param>
        public void HandleInput(GameTime gameTime)
        {
            // Delegate to InputHandler.HandleInput which will:
            // 1. Check each button state
            // 2. Execute the corresponding command if a button was pressed
            // See TheCommandPattern\Input\InputHandler.cs:HandleInput method
            _inputHandler.HandleInput(gameTime);
        }

        /// <summary>
        /// Implements the jump action logic; called by JumpCommand when executed.
        /// Displays a message via MessageManager and logs to debug output.
        /// 
        /// Called from:
        /// - TheCommandPattern\Input\Commands\JumpCommand.cs:Execute method when X button is pressed
        /// </summary>
        public void Jump()
        {
            Debug.WriteLine("Character jumped!");
            
            // Display the message on screen using the MessageManager singleton
            // See TheCommandPattern\UI\MessageManager.cs:DisplayMessage method
            MessageManager.Instance?.DisplayMessage("Character jumped!");
        }

        // Add SwapWeapon method that will be called by SwapWeaponCommand
        public void SwapWeapon()
        {
            Debug.WriteLine("Weapon swapped!");
            
            // Display the message on screen
            MessageManager.Instance?.DisplayMessage("Weapon swapped!");
        }

        // Add LurchIneffectively method that will be called by LurchIneffectivelyCommand
        public void LurchIneffectively()
        {
            Debug.WriteLine("Character lurched... ineffectively!");
            
            // Display the message on screen
            MessageManager.Instance?.DisplayMessage("Character lurched... ineffectively!");
        }
    }
}