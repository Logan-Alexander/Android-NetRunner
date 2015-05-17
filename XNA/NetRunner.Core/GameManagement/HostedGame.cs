using NetRunner.Core.Actions;
using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    public class HostedGame
    {
        public GameContext Context { get; private set; }
        public StateMachine StateMachine { get; private set; }
        private List<ICorporationConnectorServerSide> mCorporationConnectors;

        public HostedGame(GameContext gameContext, StateMachine stateMachine)
        {
            if (gameContext == null)
            {
                throw new ArgumentNullException("gameContext");
            }

            Context = gameContext;
            StateMachine = stateMachine;
            mCorporationConnectors = new List<ICorporationConnectorServerSide>();
        }

        public void AddCorporationConnector(ICorporationConnectorServerSide connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("connector");
            }

            mCorporationConnectors.Add(connector);
            connector.GameStateRequested += Connector_GameStateRequested;
            connector.ActionReceived += Connector_ActionReceived;
        }

        private void Connector_GameStateRequested(object sender, EventArgs e)
        {
            ICorporationConnectorServerSide connector = (ICorporationConnectorServerSide)sender;

            SendGameState(connector);
        }

        private void SendGameState(ICorporationConnectorServerSide connector)
        {
            CorporationGameState gameState = new CorporationGameState(Context, StateMachine);

            connector.SendGameState(gameState);
        }

        private void Connector_ActionReceived(object sender, ActionEventArgs e)
        {
            ActionBase action = e.Action;
            
            // Ensure the action is valid to be taken.
            if (!action.IsValid(Context, StateMachine))
            {
                // Tell the corporation client to resync.
                ICorporationConnectorServerSide connector = (ICorporationConnectorServerSide)sender;
                SendGameState(connector);
                return;
            }

            action.Apply(Context, StateMachine);

            // Replay the action to all connectors.
            // This will allow the corporation to confirm that action was received and
            // applied.
            foreach (var corporationConnector in mCorporationConnectors)
            {
                corporationConnector.SendAction(action);
            }
            
            // TODO: Send the action to all runner connectors.

            // Check if the hosted game itself needs to do anything.
            switch (StateMachine.State)
            {
                case State.CorporationDrawPhaseDrawingCard:
                    DrawCorporationCard();
                    break;
            }
        }

        private void DrawCorporationCard()
        {
            // TODO: Handle the runner's victory condition.
            //if (Context.ResearchAndDevelopment.IsEmpty)
            //{
            //    Runner wins.
            //}

            CorporationDrawsCard corporationDrawsCard = new CorporationDrawsCard();
            corporationDrawsCard.Apply(Context, StateMachine);

            // TODO: Tell the corporation about it (action + card name)
            MakeCardVisible makeCardVisible = new MakeCardVisible(null, null);
            foreach (var corporationConnector in mCorporationConnectors)
            {
                corporationConnector.SendAction(makeCardVisible);
                corporationConnector.SendAction(corporationDrawsCard);
            }

            // TODO: Tell the runner about it (action only)
            //foreach (var runnerConnector in mRunnerConnectors)
            //{
            //    runnerConnector.SendAction(corporationDrawsCard);
            //}
        }
    }
}
