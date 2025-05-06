using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;  // Add missing using directive for Debug

namespace TheCommandPattern.Background
{
    public class BackgroundManager
    {
        private static BackgroundManager _instance;
        public static BackgroundManager Instance => _instance ??= new BackgroundManager();

        private Texture2D _pixel;
        private BackgroundColor _currentColor = BackgroundColor.Blue;
        
        private BackgroundManager() { }

        public void Initialize(Texture2D pixel)
        {
            _pixel = pixel;
        }

        public void ChangeColor(BackgroundColor color)
        {
            _currentColor = color;
            Debug.WriteLine($"Background color changed to {_currentColor}");
        }

        public Color CurrentColor => Colors.GetColor(_currentColor);
    }
}