using Microsoft.Xna.Framework;

namespace TheCommandPattern.Background
{
    public enum BackgroundColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public static class Colors
    {
        public static Color GetColor(BackgroundColor backgroundColor)
        {
            return backgroundColor switch
            {
                BackgroundColor.Red => Color.Red,
                BackgroundColor.Green => Color.Green,
                BackgroundColor.Blue => Color.Blue,
                BackgroundColor.Yellow => Color.Yellow,
                _ => Color.CornflowerBlue
            };
        }
    }
}