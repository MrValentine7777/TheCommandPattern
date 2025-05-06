using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TheCommandPattern.Background
{
    /// <summary>
    /// Manages the background color of the game window.
    /// Implements the Singleton pattern to provide a centralized color management system
    /// that can be accessed by any component (particularly command classes).
    /// 
    /// Related files:
    /// - TheCommandPattern\Background\Colors.cs - defines color options
    /// - TheCommandPattern\Game1.cs - initializes this manager and uses its color for rendering
    /// - TheCommandPattern\Input\Commands\*.cs - commands that change the background color
    /// </summary>
    public class BackgroundManager
    {
        /// <summary>
        /// Singleton implementation - allows global access to BackgroundManager
        /// through BackgroundManager.Instance without creating multiple instances
        /// </summary>
        private static BackgroundManager _instance;
        
        /// <summary>
        /// Public accessor for the singleton instance.
        /// Creates the instance if it doesn't exist yet.
        /// Used by command classes to change background color.
        /// </summary>
        public static BackgroundManager Instance => _instance ??= new BackgroundManager();

        /// <summary>
        /// Reference to a 1x1 white pixel texture used for rendering.
        /// Initialized in Game1.cs and passed to Initialize().
        /// </summary>
        private Texture2D _pixel;
        
        /// <summary>
        /// The current background color of the game.
        /// Changed by command classes and used by Game1.cs for screen clearing.
        /// Default is Blue.
        /// </summary>
        private BackgroundColor _currentColor = BackgroundColor.Blue;
        
        /// <summary>
        /// Private constructor prevents direct instantiation.
        /// Part of the Singleton pattern implementation.
        /// </summary>
        private BackgroundManager() { }

        /// <summary>
        /// Initializes the BackgroundManager with required resources.
        /// Called from Game1.cs in the LoadContent method.
        /// </summary>
        /// <param name="pixel">1x1 white pixel texture for rendering</param>
        public void Initialize(Texture2D pixel)
        {
            _pixel = pixel;
        }

        /// <summary>
        /// Changes the current background color.
        /// Called by command classes when certain buttons are pressed.
        /// 
        /// Called from:
        /// - TheCommandPattern\Input\Commands\JumpCommand.cs - changes to Blue
        /// - TheCommandPattern\Input\Commands\FireGunCommand.cs - changes to Yellow
        /// - TheCommandPattern\Input\Commands\SwapWeaponCommand.cs - changes to Red
        /// - TheCommandPattern\Input\Commands\LurchIneffectivelyCommand.cs - changes to Green
        /// - TheCommandPattern\Input\Commands\ChangeBackgroundColorCommand.cs - changes to specified color
        /// </summary>
        /// <param name="color">The new background color</param>
        public void ChangeColor(BackgroundColor color)
        {
            _currentColor = color;
            Debug.WriteLine($"Background color changed to {_currentColor}");
        }

        /// <summary>
        /// Property that converts the stored BackgroundColor enum to an actual Color value.
        /// Used by Game1.cs when clearing the screen.
        /// 
        /// Called from:
        /// - TheCommandPattern\Game1.cs:Draw method
        /// 
        /// Uses:
        /// - TheCommandPattern\Background\Colors.cs:GetColor method
        /// </summary>
        public Color CurrentColor => Colors.GetColor(_currentColor);
    }
}