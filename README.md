# The Command Pattern in MonoGame

This project demonstrates the implementation of the Command Pattern in a MonoGame application. The Command Pattern is a behavioral design pattern that turns a request into a stand-alone object containing all information about the request. This transformation allows you to parameterize methods with different requests, delay or queue a request's execution, and support undoable operations.

## Overview

In this demonstration, gamepad inputs are mapped to different commands that execute various actions in the game world. Each button press triggers a specific command that:

1. Performs an action
2. Changes the background color
3. Displays a message on screen

## Architecture

The project implements the Command Pattern with the following components:

### Core Command Structure

- `ICommand`: Interface that defines the contract for all commands
  - `Execute()`: Method to execute the command's action
  - `HandleInput()`: Method to check if input should trigger the command
  - `ChangeBackgroundColor()`: Method to change the background color

- `InputHandler`: Manages the input-to-command mapping and delegates button presses to the appropriate commands

- `GamePadInput`: Initializes commands and connects them to the InputHandler, also contains some of the action methods that commands call

### Command Implementations

Four concrete command classes demonstrate different actions:

1. `JumpCommand`: Maps to the X button, causes the character to jump and changes background to blue
2. `FireGunCommand`: Maps to the Y button, fires the gun and changes background to yellow
3. `SwapWeaponCommand`: Maps to the B button, swaps weapons and changes background to red
4. `LurchIneffectivelyCommand`: Maps to the A button, causes the character to lurch and changes background to green

### Visual Feedback System

- `BackgroundManager`: Singleton that manages the background color state
- `MessageManager`: Singleton that handles displaying text messages on screen

## Controller Mapping

The gamepad controls are mapped as follows:

- **X Button**: Jump (Blue background)
- **Y Button**: Fire Gun (Yellow background)
- **B Button**: Swap Weapon (Red background)
- **A Button**: Lurch Ineffectively (Green background)
- **Back/ESC**: Exit game

## Benefits of the Command Pattern in this Project

1. **Decoupling**: Input handling is completely separated from the action execution
2. **Extensibility**: New commands can be added without modifying existing code
3. **Reusability**: Commands can be reused across different input methods
4. **Testability**: Commands can be tested independently of input handling

## Running the Project

1. Ensure you have .NET 8.0 installed
2. Make sure you have MonoGame 3.8.2 or later installed
3. Connect a gamepad or controller to your computer
4. Build and run the project
5. Press gamepad buttons to see the different commands in action

## Project Structure

- **Input**: Contains input handling and command implementations
  - **Commands**: Contains the ICommand interface and concrete command classes
- **Background**: Contains background color management
- **UI**: Contains message display functionality

## Development Notes

This project was developed with the assistance of GitHub Copilot X, which helped generate and refine code implementations, structure the architecture, and create documentation.

## References

- [Command Pattern - Design Patterns](https://refactoring.guru/design-patterns/command)
- [Game Programming Patterns: Command](https://gameprogrammingpatterns.com/command.html)
- [MonoGame Documentation](https://docs.monogame.net/)