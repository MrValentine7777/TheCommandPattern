using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheCommandPattern.Background;

namespace TheCommandPattern.Input.Commands
{
    public class ChangeBackgroundColorCommand : ICommand
    {
        private readonly BackgroundColor _color;
        private readonly Buttons _button; // Store which button this command is mapped to
        
        public ChangeBackgroundColorCommand(BackgroundColor color, Buttons button = Buttons.X)
        {
            _color = color;
            _button = button;
        }

        public void Execute()
        {
            // Direct debug output and background color change
            Debug.WriteLine($"Background color changed to {_color}");
            BackgroundManager.Instance.ChangeColor(_color);
        }

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

        public void HandleButtonInput(GamePadState currentState, GamePadState previousState)
        {
            // Implementation of interface method
            HandleInput(currentState, previousState);
        }

        public void ChangeBackgroundColor(Color color)
        {
            // Convert from Color to BackgroundColor enum
            BackgroundColor bgColor = BackgroundColor.Blue; // Default fallback
            
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

        private bool IsNewButtonPress(ButtonState currentState, ButtonState previousState)
        {
            return currentState == ButtonState.Pressed && previousState == ButtonState.Released;
        }
    }
}