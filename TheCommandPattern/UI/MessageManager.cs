using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheCommandPattern.UI
{
    public class MessageManager
    {
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

        private SpriteFont _font;
        private SpriteBatch _spriteBatch;
        private Texture2D _pixel; // Add new field for pixel texture
        private List<Message> _activeMessages = new List<Message>();

        public void Initialize(SpriteBatch spriteBatch, SpriteFont font, Texture2D pixel)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _pixel = pixel; // Store the pixel texture
        }

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

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update message timers
            for (int i = _activeMessages.Count - 1; i >= 0; i--)
            {
                var message = _activeMessages[i];
                message.RemainingTime -= deltaTime;

                if (message.RemainingTime <= 0)
                {
                    _activeMessages.RemoveAt(i);
                }
            }
        }

        public void Draw()
        {
            if (_spriteBatch == null || _font == null || _pixel == null)
            {
                Debug.WriteLine("Cannot draw messages: SpriteBatch, Font, or Pixel texture is null");
                return;
            }

            Debug.WriteLine($"Drawing {_activeMessages.Count} messages");
            float yPosition = 10;
            foreach (var message in _activeMessages)
            {
                // Measure the text to get its size
                Vector2 textSize = _font.MeasureString(message.Text);
                
                // Draw white background rectangle (slightly larger than the text)
                Rectangle backgroundRect = new Rectangle(
                    (int)message.Position.X - 2, 
                    (int)yPosition - 2,
                    (int)textSize.X + 4, 
                    (int)textSize.Y + 4);
                    
                _spriteBatch.Draw(
                    texture: _pixel, // Use the pixel texture instead of null
                    destinationRectangle: backgroundRect,
                    sourceRectangle: null,
                    color: Color.White);
                    
                // Draw text on top of the background
                _spriteBatch.DrawString(
                    _font, 
                    message.Text, 
                    new Vector2(message.Position.X, yPosition), 
                    Color.Magenta);
                    
                yPosition += _font.LineSpacing;
            }
        }

        private class Message
        {
            public string Text { get; set; }
            public float RemainingTime { get; set; }
            public Vector2 Position { get; set; }
        }
    }
}