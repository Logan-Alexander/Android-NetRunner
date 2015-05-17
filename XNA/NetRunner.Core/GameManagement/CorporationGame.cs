using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetRunner.Core.GameFlow;
using NetRunner.Core.Actions;

namespace NetRunner.Core.GameManagement
{
    /// <summary>
    /// A corporation game is a game seen from the Corporation's point of view.
    /// I.e. The Corporation's cards will be visible, but the Runner's card will not.
    /// Any action that is taken by the corporation will be sent to the hosted game.
    /// </summary>
    public class CorporationGame
    {
        private ICorporationConnectorClientSide _Connector;
        public GameContext Context { get; private set; }
        public StateMachine StateMachine { get; private set; }
        private Queue<ActionBase> _UnconfirmedActions = new Queue<ActionBase>();

        public CorporationGame(ICorporationConnectorClientSide connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("connector");
            }

            _Connector = connector;
            _Connector.ActionReceived += Connector_ActionReceived;
            _Connector.GameStateReceived += Connector_GameStateReceived;
            Resync();
        }

        private void Connector_ActionReceived(object sender, ActionEventArgs e)
        {
            if (_UnconfirmedActions.Count > 0)
            {
                ActionBase unconfirmedAction = _UnconfirmedActions.Peek();
                if (unconfirmedAction.Equals(e.Action))
                {
                    _UnconfirmedActions.Dequeue();
                }
                else
                {
                    //Resync();
                    //return;
                }
            }
            else
            {
                ApplyAction(e.Action, false);
            }
        }

        private void Connector_GameStateReceived(object sender, CorporationGameStateEventArgs e)
        {
            Load(e.CorporationGameState);
        }

        private void Load(CorporationGameState corporationGameState)
        {
            // TODO: Load the game
            
            Context = new GameContext();

            StateMachine = new StateMachine(
                corporationGameState.State,
                corporationGameState.OpponentWillHaveChanceToRespond,
                corporationGameState.OpponentHasHadFirstChanceToRespond);
        }

        public void TakeAction(ActionBase action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            ApplyAction(action, true);
        }

        private void ApplyAction(ActionBase action, bool sendToHostedGame)
        {
            // Ensure the action is valid to be taken.
            if (!action.IsValid(Context, StateMachine))
            {
                // Resync();
                throw new Exception("The action was not valid.");
            }

            // We want to see an immediate effect rather than wait for the round-trip to the server.
            action.Apply(Context, StateMachine);

            if (sendToHostedGame)
            {
                _UnconfirmedActions.Enqueue(action);

                // Send the action to the hosted game via our connector.
                _Connector.SendAction(action);
            }
        }

        public void Resync()
        {
            _Connector.RequestGameState();
        }
    }
}
