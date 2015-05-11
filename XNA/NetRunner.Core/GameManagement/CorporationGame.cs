using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    /// <summary>
    /// A corporation game is a game seen from the Corporation's point of view.
    /// I.e. The Corporation's cards will be visible, but the Runner's card will not.
    /// Any action that is taken by the corporation will be sent to the hosted game.
    /// </summary>
    public class CorporationGame
    {
        private ICorporationConnectorClientSide mConnector;

        public CorporationGame(ICorporationConnectorClientSide connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("connector");
            }

            mConnector = connector;
            mConnector.ActionReceived += Connector_ActionReceived;
            mConnector.GameStateReceived += Connector_GameStateReceived;
            Resync();
        }

        private void Connector_ActionReceived(object sender, ActionEventArgs e)
        {
            // Check action matches the expectation.
            // Apply action.
        }

        private void Connector_GameStateReceived(object sender, CorporationGameStateEventArgs e)
        {
            Load(e.CorporationGameState);
        }

        private void Load(CorporationGameState corporationGameState)
        {
            // TODO: Load the game
        }

        public void TakeAction(ActionBase action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            // TODO: Check that the action is legal.
            //       If it's not, we probably need to call Resync().

            // TODO: Apply the action locally.
            //       We want to see an immediate effect rather than wait for the round-trip to the server.

            // TODO: Set an expectation that the next action reported by the hosted game will be this action.
            //       If the server tells us that our action was invalid, we probably need to call Resync();
            
            // Send the action to the hosted game via our connector.
            mConnector.SendAction(action);
        }

        public void Resync()
        {
            mConnector.RequestGameState();
        }
    }
}
