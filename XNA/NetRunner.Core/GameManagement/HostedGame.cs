using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    public class HostedGame
    {
        public GameContext GameContext { get; set; }
        private List<ICorporationConnectorServerSide> mCorporationConnectors;

        public HostedGame(GameContext gameContext)
        {
            if (gameContext == null)
            {
                throw new ArgumentNullException("gameContext");
            }

            GameContext = gameContext;
            mCorporationConnectors = new List<ICorporationConnectorServerSide>();
        }

        public void AddCorporationConnector(ICorporationConnectorServerSide connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("connector");
            }

            connector.GameStateRequested += Connector_GameStateRequested;
            connector.ActionReceived += Connector_ActionReceived;
        }

        private void Connector_GameStateRequested(object sender, EventArgs e)
        {
            ICorporationConnectorServerSide connector = (ICorporationConnectorServerSide)sender;
            
            CorporationGameState gameState = new CorporationGameState(GameContext);

            connector.SendGameState(gameState);
        }

        private void Connector_ActionReceived(object sender, ActionEventArgs e)
        {
            // TODO: Handle the action made by the corporation.
        }
    }
}
