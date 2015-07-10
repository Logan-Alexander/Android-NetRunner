using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrahamThomson.Xna.Common
{
    /// <summary>
    /// Provides keyboard state information.
    /// </summary>
    public interface IKeyboardManager : IGameComponent
    {
        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        KeyboardState CurrentKeyboardState { get; }

        /// <summary>
        /// The state of the keyboard from the previous update cycle.
        /// </summary>
        KeyboardState PreviousKeyboardState { get; }

        /// <summary>
        /// Returns true if a key is currently pressed.
        /// </summary>
        bool IsKeyDown(Keys key);

        /// <summary>
        /// Returns whether a key has been newly pressed since the last update.
        /// </summary>
        /// <param name="key">The key to test.</param>
        /// <param name="suppress">Indicates that the key should be suppressed 
        /// for the rest of this update cycle. This prevents other components
        /// responding to the same key press.</param>
        bool IsKeyPressed(Keys key, bool suppress = true);
    }

    /// <summary>
    /// Implementation of the IKeyboardManager interdace which
    /// provides keyboard state information.
    /// </summary>
    public class KeyboardManager : GameComponent, IKeyboardManager
    {
        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        public KeyboardState CurrentKeyboardState { get; private set; }

        /// <summary>
        /// The state of the keyboard from the previous update cycle.
        /// </summary>
        public KeyboardState PreviousKeyboardState { get; private set; }

        /// <summary>
        /// Stores a list of each key that has been read this cycle.
        /// This prevents two components responding to the same key press
        /// if the first key press triggers a change of game state.
        /// </summary>
        private List<Keys> _SuppressedKeys = new List<Keys>();

        /// <summary>
        /// Creates a new KeyboardManager and registers it to handle
        /// the keyboard input for the specified Game.
        /// </summary>
        /// <param name="game">Game the KeyboardManager should be associated with.</param>
        public KeyboardManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IKeyboardManager), this);
            Game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
            _SuppressedKeys.Clear();

            base.Update(gameTime);
        }

        /// <summary>
        /// Returns true if a key is currently pressed.
        /// </summary>
        public bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Returns whether a key has been newly pressed since the last update.
        /// </summary>
        /// <param name="key">The key to test.</param>
        /// <param name="suppress">Indicates that the key should be suppressed 
        /// for the rest of this update cycle. This prevents other components
        /// responding to the same key press.</param>
        public bool IsKeyPressed(Keys key, bool suppress = true)
        {
            bool result = CurrentKeyboardState.IsKeyDown(key)
                            && !PreviousKeyboardState.IsKeyDown(key)
                            && !_SuppressedKeys.Contains(key);


            if (result && suppress)
            {
                _SuppressedKeys.Add(key);
            }

            return result;
        }
    }
}
