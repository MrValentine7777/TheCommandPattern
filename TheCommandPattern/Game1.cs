global using System.Diagnostics;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
global using TheCommandPattern.Input;
global using TheCommandPattern.Input.Commands;
global using TheCommandPattern.Background;
global using TheCommandPattern.UI;

namespace TheCommandPattern
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GamePadInput gamePadInput;
        private Texture2D _pixel; // 1x1 white pixel texture
        private SpriteFont _font; // Font for displaying messages

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            gamePadInput = new GamePadInput();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a 1x1 white pixel texture
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            // Load the font
            _font = Content.Load<SpriteFont>("Font");
            
            // Initialize the background manager with the pixel texture
            BackgroundManager.Instance.Initialize(_pixel);
            
            // Initialize the message manager
            MessageManager.Instance.Initialize(_spriteBatch, _font, _pixel);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Debug.WriteLine("________________________Exiting game...");
                Exit();
            }
            // TODO: Add your update logic here

            gamePadInput.HandleInput(gameTime);
            
            // Update message manager
            MessageManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Use the current background color from BackgroundManager
            GraphicsDevice.Clear(BackgroundManager.Instance.CurrentColor);

            _spriteBatch.Begin();
            
            // Draw messages
            MessageManager.Instance.Draw();
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
