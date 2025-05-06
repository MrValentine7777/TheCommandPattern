// Global usings provide access to core libraries and project namespaces
// across the entire project without needing individual using statements
global using System.Diagnostics;      // For Debug.WriteLine logging throughout the app
global using Microsoft.Xna.Framework; // Core MonoGame framework
global using Microsoft.Xna.Framework.Graphics; // For rendering (SpriteBatch, etc.)
global using Microsoft.Xna.Framework.Input; // For handling gamepad/keyboard input
global using TheCommandPattern.Input; // Custom input handling system (GamePadInput.cs)
global using TheCommandPattern.Input.Commands; // Command pattern implementation (ICommand.cs and concrete commands)
global using TheCommandPattern.Background; // Background color management (BackgroundManager.cs, Colors.cs)
global using TheCommandPattern.UI; // UI elements like MessageManager.cs

namespace TheCommandPattern
{
    /// <summary>
    /// Main game class that coordinates all systems.
    /// Serves as the entry point and manages the game loop.
    /// 
    /// Key relationships:
    /// - Creates and initializes GamePadInput (TheCommandPattern\Input\GamePadInput.cs)
    /// - Initializes BackgroundManager (TheCommandPattern\Background\BackgroundManager.cs)
    /// - Initializes MessageManager (TheCommandPattern\UI\MessageManager.cs)
    /// - Manages game loop (Update and Draw methods)
    /// 
    /// Implements the Command Pattern:
    /// - GamePadInput maps gamepad buttons to ICommand instances (e.g., JumpCommand, FireGunCommand)
    /// - Commands execute actions and interact with other components (e.g., changing background color)
    /// 
    /// Singleton Pattern Usage:
    /// - BackgroundManager and MessageManager are implemented as singletons to ensure global access
    /// </summary>
    public class Game1 : Game
    {
        // Core MonoGame graphics management
        private GraphicsDeviceManager _graphics;
        
        // Used for all 2D rendering operations, shared with MessageManager
        private SpriteBatch _spriteBatch;
        
        // Handles gamepad input and command pattern implementation
        // See TheCommandPattern\Input\GamePadInput.cs
        private GamePadInput gamePadInput;
        
        // Reusable white pixel texture, shared with BackgroundManager and MessageManager
        // for rendering colored rectangles and backgrounds
        private Texture2D _pixel;
        
        // Font used for rendering text messages
        // Loaded from TheCommandPattern\Content\Font.spritefont
        private SpriteFont _font;

        /// <summary>
        /// Constructor initializes core game components and creates GamePadInput.
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Create GamePadInput which instantiates commands for buttons
            // See TheCommandPattern\Input\GamePadInput.cs constructor
            gamePadInput = new GamePadInput();
        }

        /// <summary>
        /// Initialize is called once before the game loop begins.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent creates and loads all game resources.
        /// Initializes managers that depend on graphics resources.
        /// </summary>
        protected override void LoadContent()
        {
            // Create SpriteBatch used throughout the game for rendering
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a 1x1 white pixel texture that's used by:
            // - BackgroundManager.cs for clearing the screen
            // - MessageManager.cs for drawing message backgrounds
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            // Load the font from Content\Font.spritefont
            // This is used by MessageManager.cs for rendering text messages
            _font = Content.Load<SpriteFont>("Font");
            
            // Initialize the background manager with the pixel texture
            // See TheCommandPattern\Background\BackgroundManager.cs
            BackgroundManager.Instance.Initialize(_pixel);
            
            // Initialize the message manager with required dependencies
            // See TheCommandPattern\UI\MessageManager.cs
            MessageManager.Instance.Initialize(_spriteBatch, _font, _pixel);
        }

        /// <summary>
        /// Update is called once per frame to update game state.
        /// Processes input and updates managers.
        /// </summary>
        /// <param name="gameTime">Timing values for the current frame</param>
        protected override void Update(GameTime gameTime)
        {
            // Check for exit conditions
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Debug.WriteLine("________________________Exiting game...");
                Exit();
            }

            // Process gamepad input through the Command Pattern
            // This triggers commands like JumpCommand, FireGunCommand, etc.
            // See TheCommandPattern\Input\GamePadInput.cs and the Command classes
            gamePadInput.HandleInput(gameTime);
            
            // Update message display timers and remove expired messages
            // See TheCommandPattern\UI\MessageManager.cs
            MessageManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw is called once per frame to render the game.
        /// Sets background color and coordinates rendering of UI elements.
        /// </summary>
        /// <param name="gameTime">Timing values for the current frame</param>
        protected override void Draw(GameTime gameTime)
        {
            // Use the current background color from BackgroundManager
            // Color changes when commands like ChangeBackgroundColorCommand execute
            // See TheCommandPattern\Background\BackgroundManager.cs
            GraphicsDevice.Clear(BackgroundManager.Instance.CurrentColor);

            // Begin the sprite batch for drawing UI elements
            _spriteBatch.Begin();
            
            // Draw on-screen messages from MessageManager
            // This renders text from commands like FireGunCommand
            // See TheCommandPattern\UI\MessageManager.cs
            MessageManager.Instance.Draw();
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
