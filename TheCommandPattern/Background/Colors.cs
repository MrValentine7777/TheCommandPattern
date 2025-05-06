using Microsoft.Xna.Framework;

namespace TheCommandPattern.Background
{
    /// <summary>
    /// Defines the available background colors for the game.
    /// Used by BackgroundManager and command classes to specify colors.
    /// 
    /// Related files:
    /// - TheCommandPattern\Background\BackgroundManager.cs - uses these colors
    /// - TheCommandPattern\Input\Commands\*.cs - command classes reference these colors
    /// </summary>
    public enum BackgroundColor
    {
        /// <summary>Red background - typically used by SwapWeaponCommand</summary>
        Red,
        
        /// <summary>Green background - typically used by LurchIneffectivelyCommand</summary>
        Green,
        
        /// <summary>Blue background - typically used by JumpCommand</summary>
        Blue,
        
        /// <summary>Yellow background - typically used by FireGunCommand</summary>
        Yellow
    }

    /// <summary>
    /// Utility class that maps BackgroundColor enum values to XNA Color objects.
    /// Provides a consistent color mapping across the application.
    /// 
    /// Related files:
    /// - TheCommandPattern\Background\BackgroundManager.cs - uses GetColor method
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// Converts a BackgroundColor enum value to an XNA Color object.
        /// Used by BackgroundManager.CurrentColor property.
        /// 
        /// Called from:
        /// - TheCommandPattern\Background\BackgroundManager.cs:CurrentColor property
        /// </summary>
        /// <param name="backgroundColor">BackgroundColor enum value to convert</param>
        /// <returns>The corresponding XNA Color</returns>
        public static Color GetColor(BackgroundColor backgroundColor)
        {
            // Use modern C# switch expression to map enum values to Colors
            return backgroundColor switch
            {
                BackgroundColor.Red => Color.Red,
                BackgroundColor.Green => Color.Green,
                BackgroundColor.Blue => Color.Blue,
                BackgroundColor.Yellow => Color.Yellow,
                _ => Color.CornflowerBlue // Default fallback
            };
        }
    }
}