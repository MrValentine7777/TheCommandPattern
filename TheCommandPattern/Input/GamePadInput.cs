// TheCommandPattern\Input\GamePadInput.cs
using System.Diagnostics;
using Microsoft.Xna.Framework;
using TheCommandPattern.Background;
using TheCommandPattern.Input.Commands;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.UI;

namespace TheCommandPattern.Input
{
    public class GamePadInput
    {
        private InputHandler _inputHandler;

        public GamePadInput()
        {
            // Create command instances with reference to this GamePadInput instance
            var jumpCommand = new JumpCommand(this);
            var fireGunCommand = new FireGunCommand();
            var swapWeaponCommand = new SwapWeaponCommand(this);
            var lurchCommand = new LurchIneffectivelyCommand(this);

            // Initialize the InputHandler with concrete command implementations
            // Use the specific command classes for each button
            _inputHandler = new InputHandler(
                jumpCommand,         // X button
                fireGunCommand,      // Y button
                lurchCommand,        // A button
                swapWeaponCommand    // B button
            );
        }

        // Handle input using the command pattern
        public void HandleInput(GameTime gameTime)
        {
            _inputHandler.HandleInput(gameTime);
        }

        // Add Jump method that will be called by JumpCommand
        public void Jump()
        {
            Debug.WriteLine("Character jumped!");
            
            // Display the message on screen (if MessageManager is used in the project)
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