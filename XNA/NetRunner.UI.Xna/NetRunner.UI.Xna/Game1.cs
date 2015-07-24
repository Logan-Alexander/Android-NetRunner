using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NetRunner.UI.Xna.Components;
using GrahamThomson.Xna.Common;
using NetRunner.Core.Actions;
using System.Diagnostics;
using NetRunner.Core.CardIdentifiers;

namespace NetRunner.UI.Xna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font1;

        private ConsoleUI _Console;
        private KeyboardManager _KeyboardManager;
        private LocalGameComponent _LocalGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            _Console = new ConsoleUI(this);
            _KeyboardManager = new KeyboardManager(this);
            _LocalGame = new LocalGameComponent(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font1 = Content.Load<SpriteFont>("Fonts/SpriteFont1");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (_KeyboardManager.IsKeyPressed(Keys.Escape, true))
                this.Exit();

            // TODO: Add your update logic here
            if (_KeyboardManager.IsKeyPressed(Keys.D1, true))
            {
                Debug.WriteLine("Corporation passes.");
                _LocalGame.TakeCorporationAction(new CorporationPasses());
            }

            if (_KeyboardManager.IsKeyPressed(Keys.D2, true))
            {
                Debug.WriteLine("Runner passes.");
                _LocalGame.TakeRunnerAction(new RunnerPasses());
            }

            if (_KeyboardManager.IsKeyPressed(Keys.D3, true))
            {
                Debug.WriteLine("Corporation draws card.");
                _LocalGame.TakeCorporationAction(new CorporationDrawsCard());
            }

            if (_KeyboardManager.IsKeyPressed(Keys.D4, true))
            {
                Debug.WriteLine("Corporation scores agenda.");
                AssetOrAgendaIdentifier cardIdentifier = new AssetOrAgendaIdentifier(0);
                _LocalGame.TakeCorporationAction(new CorporationScoresAgenda(cardIdentifier));
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            int bottom = GraphicsDevice.Viewport.TitleSafeArea.Bottom;

            string state = _LocalGame.CorporationGame.Flow.ToString();
            spriteBatch.DrawString(font1, state, new Vector2(0, bottom - 32), Color.White);

            int corpHandCount = _LocalGame.CorporationGame.Context.HeadQuarters.Hand.Count;
            string corpInfo = string.Format("The corporation has {0} card(s) in their hand.", corpHandCount);
            spriteBatch.DrawString(font1, corpInfo, new Vector2(0, bottom - 16), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
