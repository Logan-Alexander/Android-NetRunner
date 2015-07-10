using Microsoft.Xna.Framework;
using NetRunner.Core;
using NetRunner.Core.GameFlow;
using NetRunner.Core.GameManagement;
using NetRunner.Core.GameManagement.InMemory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    public class LocalGameComponent : GameComponent, IRunnerGameService, ICorporationGameService
    {
        public RunnerGame RunnerGame { get; private set; }
        public CorporationGame CorporationGame { get; private set; }

        private HostedGame _HostedGame;
        private InMemoryGameConnector _Connector;

        public LocalGameComponent(Game game)
            : base(game)
        {
            Game.Components.Add(this);
            Game.Services.AddService(typeof(IRunnerGameService), this);
            Game.Services.AddService(typeof(ICorporationGameService), this);
        }

        public override void Initialize()
        {
            base.Initialize();

            CreateHostedGame();
            CreateCorporationGame();
            CreateRunnerGame();
        }

        private void CreateHostedGame()
        {
            // Create the GameContext. This will hold all information about the game.
            GameContext gameContext = new GameContext();
            Flow stack = new Flow();
            stack.Fire(Trigger.GameStarts);

            // Create a HostedGame.
            // This will allow information about the game to be broadcast to all players.
            // The information sent to the Runner and the Corporation will be different.
            // For example, when the Corporation draws a card, the Corporation will be told
            // which card was moved from R&D to Headquarters. The Runner will only be told
            // that a card was moved.
            _HostedGame = new HostedGame(gameContext, stack);

            // An "in-memory" game connector allows information to flow directly from clients
            // to the hosted game. In a local game, this will be sufficient.
            // For an internet game, the web server would create an instance
            // of some connector that implemented the server-side interface.
            _Connector = new InMemoryGameConnector();

            // Tell the hosted game about our connector so that it handles our requests and
            // broadcasts information to us.
            _HostedGame.AddCorporationConnector(_Connector);
            _HostedGame.AddRunnerConnector(_Connector);
        }

        private void CreateCorporationGame()
        {
            CorporationGame = new CorporationGame(_Connector);
            CorporationGame.GameOutOfSync += delegate { Debug.WriteLine("Corporation game out of sync."); };
        }

        private void CreateRunnerGame()
        {
            RunnerGame = new RunnerGame(_Connector);
            RunnerGame.GameOutOfSync += delegate { Debug.WriteLine("Runner game out of sync."); };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _Connector.Update();
        }

        public void TakeCorporationAction(Core.Actions.ActionBase action)
        {
            CorporationGame.TakeAction(action);
        }

        public void TakeRunnerAction(Core.Actions.ActionBase action)
        {
            RunnerGame.TakeAction(action);
        }
    }
}
