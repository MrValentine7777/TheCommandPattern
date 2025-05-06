using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Input.Commands;

namespace TheCommandPattern.Input
{
    public class InputHandler
    {
        // C# equivalent of the C++ InputHandler class
        private ICommand _buttonX;
        private ICommand _buttonY;
        private ICommand _buttonA;
        private ICommand _buttonB;
        
        // Store previous state to detect button releases
        private GamePadState _previousState;

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

        public void HandleInput(GameTime gameTime)
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Delegate button press detection to the command classes
            _buttonX.HandleInput(currentState, _previousState);
            _buttonY.HandleInput(currentState, _previousState);
            _buttonB.HandleInput(currentState, _previousState);
            _buttonA.HandleInput(currentState, _previousState);

            // Store current state for next frame comparison
            _previousState = currentState;
        }
    }
}