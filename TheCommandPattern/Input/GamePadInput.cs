using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCommandPattern.Input
{
    internal class GamePadInput
    {
        // Store previous state to detect button releases
        private GamePadState _previousState;

        // get the gamepad player one state
        public GamePadState GetGamePadState(GameTime gameTime)
        {
            Debug.WriteLine("________________________GamePadState: " + GamePad.GetState(PlayerIndex.One).ToString());

            return GamePad.GetState(PlayerIndex.One);
        }

        // this is the cpp code the pattern is based off of
        //void InputHandler::handleInput()
        //{
        //    if (isPressed(BUTTON_X)) jump();
        //    else if (isPressed(BUTTON_Y)) fireGun();
        //    else if (isPressed(BUTTON_B)) swapWeapon();
        //    else if (isPressed(BUTTON_Y)) lurchIneffectively();
        //}

        // Handle input using MonoGame's GamePadState
        public void HandleInput(GameTime gameTime)
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Check for new button presses (pressed now but not in previous frame)
            if (IsNewButtonPress(currentState.Buttons.X, _previousState.Buttons.X))
                Jump();
            else if (IsNewButtonPress(currentState.Buttons.Y, _previousState.Buttons.Y))
                FireGun();
            else if (IsNewButtonPress(currentState.Buttons.B, _previousState.Buttons.B))
                SwapWeapon();
            else if (IsNewButtonPress(currentState.Buttons.A, _previousState.Buttons.A))
                LurchIneffectively();

            // Store current state for next frame comparison
            _previousState = currentState;
        }

        // Helper method to check if a button is pressed
        private bool IsPressed(ButtonState buttonState)
        {
            return buttonState == ButtonState.Pressed;
        }

        // Helper method to detect new button presses
        private bool IsNewButtonPress(ButtonState currentState, ButtonState previousState)
        {
            // Only return true if button is pressed now but wasn't pressed in previous frame
            return currentState == ButtonState.Pressed && previousState == ButtonState.Released;
        }

        private void Jump()
        {
            Debug.WriteLine("Player jumped");
            // Implementation for jump action
        }

        private void FireGun()
        {
            Debug.WriteLine("Player fired gun");
            // Implementation for firing gun action
        }

        private void SwapWeapon()
        {
            Debug.WriteLine("Player swapped weapon");
            // Implementation for swapping weapon action
        }

        private void LurchIneffectively()
        {
            Debug.WriteLine("Player lurched ineffectively");
            // Implementation for lurching ineffectively action
        }

        class Command
        {
            public:
                virtual ~Command() { }
            virtual void execute() = 0;
        };

        class JumpCommand : public Command
        {
            public:
                virtual void execute() jump();
        };

        class FireCommand : public Command
        {
                    public:
                        virtual void execute() fireGun();
                };

class SwapWeaponCommand : public Command
{
            public:
                virtual void execute() swapWeapon();
        };

class LurchIneffectivelyCommand : public Command
{
                public:
                 virtual void execute() lurchIneffectively();
          };

class InputHandler
{
    public:
        void handleInput();

    private:
        Command* buttonX_;
    Command* buttonY_;
    Command* buttonA_;
    Command* buttonB_;
};

void InputHandler::handleInput()
        {
            if (isPressed(BUTTON_X)) buttonX_->execute();
            else if (isPressed(BUTTON_Y)) buttonY_->execute();
            else if (isPressed(BUTTON_B)) buttonB_->execute();
            else if (isPressed(BUTTON_A)) buttonA_->execute();
}
    }
}
