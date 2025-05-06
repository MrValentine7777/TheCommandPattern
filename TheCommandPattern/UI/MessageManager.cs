using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheCommandPattern.UI
{
    /// <summary>
    /// Manages on-screen message display throughout the game.
    /// Implements the Singleton pattern to provide a centralized message system
    /// that can be accessed by any component (particularly command classes).
    /// 
    /// Related files:
    /// - TheCommandPattern\Game1.cs - initializes and updates this manager
    /// - TheCommandPattern\Input\GamePadInput.cs - uses DisplayMessage for button actions
    /// - TheCommandPattern\Input\Commands\FireGunCommand.cs - directly calls DisplayMessage
    /// </summary>
    public class MessageManager
    {
        // Singleton implementation - allows global access to MessageManager
        // through MessageManager.Instance without creating multiple instances
        private static MessageManager _instance;
        public static MessageManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MessageManager();
                return _instance;
            }
        }

        // Dependencies required for drawing - initialized in Game1.cs:LoadContent()
        private SpriteFont _font;        // Font loaded from Content/Font.spritefont
        private SpriteBatch _spriteBatch; // Used for rendering text and rectangles
        private Texture2D _pixel;         // 1x1 white texture for drawing backgrounds

        // Collection of all messages currently visible on screen
        private List<Message> _activeMessages = new List<Message>();

        /// <summary>
        /// Initializes the MessageManager with required dependencies.
        /// Called from Game1.cs in the LoadContent method.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch for rendering</param>
        /// <param name="font">SpriteFont loaded from Content</param>
        /// <param name="pixel">1x1 white texture for backgrounds</param>
        public void Initialize(SpriteBatch spriteBatch, SpriteFont font, Texture2D pixel)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _pixel = pixel; // Used for drawing message backgrounds
        }

        /// <summary>
        /// Displays a message on screen for a specified duration.
        /// Called by command classes (e.g., FireGunCommand, JumpCommand) 
        /// when player actions trigger messages.
        /// 
        /// See:
        /// - TheCommandPattern\Input\GamePadInput.cs
        /// - TheCommandPattern\Input\Commands\FireGunCommand.cs
        /// </summary>
        /// <param name="text">Message text to display</param>
        /// <param name="duration">How long to display the message in seconds</param>
        public void DisplayMessage(string text, float duration = 2.0f)
        {
            _activeMessages.Add(new Message
            {
                Text = text,
                RemainingTime = duration,
                Position = new Vector2(10, 10) // Position at the top left
            });
            Debug.WriteLine($"Message added: {text}, Active messages: {_activeMessages.Count}");
        }

        /// <summary>
        /// Updates all active messages, removing any that have expired.
        /// Called each frame from Game1.cs:Update()
        /// </summary>
        /// <param name="gameTime">Game timing information</param>
        public void Update(GameTime gameTime)
        {
            // Convert to seconds for timing calculations
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update message timers - iterate backwards to safely remove items
            for (int i = _activeMessages.Count - 1; i >= 0; i--)
            {
                var message = _activeMessages[i];
                message.RemainingTime -= deltaTime;

                // Remove expired messages
                if (message.RemainingTime <= 0)
                {
                    _activeMessages.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Renders all active messages to the screen.
        /// Called from Game1.cs:Draw() after SpriteBatch.Begin()
        /// </summary>
        public void Draw()
        {
            // Safety check to prevent rendering errors
            if (_spriteBatch == null || _font == null || _pixel == null)
            {
                Debug.WriteLine("Cannot draw messages: SpriteBatch, Font, or Pixel texture is null");
                return;
            }

            Debug.WriteLine($"Drawing {_activeMessages.Count} messages");

            // Position messages in a vertical stack
            float yPosition = 10;
            foreach (var message in _activeMessages)
            {
                // Measure the text to get its size for background rectangle
                Vector2 textSize = _font.MeasureString(message.Text);

                // Draw white background rectangle (slightly larger than the text)
                // This creates a text box effect to improve readability
                Rectangle backgroundRect = new Rectangle(
                    (int)message.Position.X - 2,
                    (int)yPosition - 2,
                    (int)textSize.X + 4,
                    (int)textSize.Y + 4);

                _spriteBatch.Draw(
                    texture: _pixel, // 1x1 white pixel created in Game1.cs
                    destinationRectangle: backgroundRect,
                    sourceRectangle: null,
                    color: Color.White);

                // Draw text on top of the background with magenta color for visibility
                _spriteBatch.DrawString(
                    _font,
                    message.Text,
                    new Vector2(message.Position.X, yPosition),
                    Color.Magenta);

                // Move down for next message using the font's line spacing
                yPosition += _font.LineSpacing;
            }
        }

        /// <summary>
        /// Internal representation of an on-screen message.
        /// Stores text content, position, and lifetime information.
        /// </summary>
        private class Message
        {
            // The text content to display
            public string Text { get; set; }

            // How much longer this message should remain visible (in seconds)
            public float RemainingTime { get; set; }

            // Where to position the message on screen (top-left corner)
            public Vector2 Position { get; set; }
        }
    }
}