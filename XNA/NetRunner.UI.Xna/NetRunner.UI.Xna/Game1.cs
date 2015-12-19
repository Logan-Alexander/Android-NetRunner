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
using NetRunner.UI.Xna.Layout;
using NetRunner.Core.GameFlow;

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
        Texture2D _TemporaryTexture;

        private Background _Background;
        private ConsoleUI _Console;
        private KeyboardManager _KeyboardManager;
        private LocalGameComponent _LocalGame;
        private Camera _Camera;
        private LayoutService _LayoutService;
        private CardsUI _Cards;
        private VisualManager _ActionManager;
        private NetRunnerSoundManager _NetRunnerSoundManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Window.Title = "Android: NetRunner";

            graphics.IsFullScreen = false;
            Window.AllowUserResizing = true;
            System.Windows.Forms.Form form = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(Window.Handle);
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            _Background = new Background(this);
            _KeyboardManager = new KeyboardManager(this);
            _LocalGame = new LocalGameComponent(this);
            _Camera = new Camera(this);
            _Cards = new CardsUI(this);
            _ActionManager = new VisualManager(this);
            _NetRunnerSoundManager = new NetRunnerSoundManager(this);
            _LayoutService = new LayoutService(this);

            _Console = new ConsoleUI(this);

            // TEMP: Choose which background to draw!
            _Background.Player = Background.PlayerType.Corporation;
            //_Background.Player = Background.PlayerType.Runner;
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

            // Set the camera to be 0.5m above the table, looking down.
            // For the camera, we want "up" to be the edge of the table that's
            // far away from us, which in our 3D world is the direction of the -Z axis.
            _Camera.Set(new Vector3(0, 0.5f, 0.5f), new Vector3(0, 0, 0), new Vector3(0, 0, -1));
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

            // Create a 1x1 white texture.
            _TemporaryTexture= new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _TemporaryTexture.SetData(new Color[1] { Color.White });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            if (_TemporaryTexture != null)
            {
                _TemporaryTexture.Dispose();
                _TemporaryTexture = null;
            }
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
                if (_LocalGame.CorporationGame.Flow.CanFire(Trigger.CorporationDrawsCardAtStartOfTurn))
                {
                    _LocalGame.TakeCorporationAction(new CorporationDrawsCardAtStartOfTurn());
                }
                else
                {
                    _LocalGame.TakeCorporationAction(new CorporationDrawsCardAsAction());
                }
            }

            if (_KeyboardManager.IsKeyPressed(Keys.D4, true))
            {
                Debug.WriteLine("Corporation scores agenda.");
                AssetOrAgendaIdentifier cardIdentifier = new AssetOrAgendaIdentifier(0);
                _LocalGame.TakeCorporationAction(new CorporationScoresAgenda(cardIdentifier));
            }

            if (_KeyboardManager.IsKeyPressed(Keys.D5, true))
            {
                Debug.WriteLine("Corporation take one credit.");
                _LocalGame.TakeCorporationAction(new CorporationTakesOneCredit());
            }

            if (_KeyboardManager.IsKeyPressed(Keys.D6, true))
            {
                Debug.WriteLine("Corporation disacrds card.");
                HQCardIdentifier cardIdentifier = new HQCardIdentifier(0);
                _LocalGame.TakeCorporationAction(new CorporationDiscardsCardFromHQ(cardIdentifier));
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            // TEMPORARY: Draw the description of the game flow.
            string state = _LocalGame.CorporationGame.Flow.ToString();
            spriteBatch.DrawString(font1, state, new Vector2(8, 0), Color.White);

            int corpHandCount = _LocalGame.CorporationGame.Context.HeadQuarters.Hand.Count;
            string corpInfo = string.Format("The corporation has {0} card(s) in their hand.", corpHandCount);
            spriteBatch.DrawString(font1, corpInfo, new Vector2(8, 16), Color.White);

            // TEMPROARY: Draw the list of available actions.
            List<string> actions = new List<string>();
            foreach (Trigger trigger in _LocalGame.CorporationGame.Flow.CurrentStateMachine.PermittedTriggers)
            {
                switch (trigger)
                {
                    case Trigger.CorporationDrawsCardAtStartOfTurn:
                        actions.Add("- Draw card at start of turn");
                        break;

                    case Trigger.CorporationPasses:
                        actions.Add("- Pass");
                        break;

                    case Trigger.CorporationRezzesNonIce:
                        actions.Add("- Rez an assert or upgrade");
                        break;

                    case Trigger.CorporationScoresAgenda:
                        actions.Add("- Score an agenda");
                        break;

                    case Trigger.CorporationUsesPaidAbility:
                        actions.Add("- Use paid ability");
                        break;

                    case Trigger.CorporationDrawsCardAsAction:
                        actions.Add("- Draw card");
                        break;

                    case Trigger.CorporationTakesOneCredit:
                        actions.Add("- Take one credit");
                        break;

                    case Trigger.CorporationDiscardsCardFromHQ:
                        actions.Add("- Discard card from HQ");
                        break;

                    default:
                        // No acitons.
                        break;
                }
            }

            int y = 32;
            foreach (string action in actions)
            {
                spriteBatch.DrawString(font1, action, new Vector2(8, y), Color.White);
                y += 32;
            }

            spriteBatch.End();
        }
    }
}
