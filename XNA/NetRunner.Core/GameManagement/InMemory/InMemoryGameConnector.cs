using NetRunner.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement.InMemory
{
    /// <summary>
    /// This connector implements both the server-side and the client-side interfaces for the Corporation.
    /// It can be used to connect a Corporation player's game to a hosted game that are running on the same
    /// computer.
    /// </summary>
    public class InMemoryGameConnector : IClient, ICorporationConnectorServerSide, ICorporationConnectorClientSide
    {
        private List<ActionBase> mCorporationActionsToSendToHostedGame = new List<ActionBase>();
        private List<ActionBase> mActionsToDelieverToCorporation = new List<ActionBase>();
        private bool mCorporationRequestedGameState;
        private List<CorporationGameState> mGameStateToDelieverToCorporation = new List<CorporationGameState>();

        #region IClient

        public void Update()
        {
            // Deliver corporation actions to hosted game.
            List<ActionBase> actionsToSend = new List<ActionBase>(mCorporationActionsToSendToHostedGame);
            mCorporationActionsToSendToHostedGame.Clear();
            foreach (ActionBase action in actionsToSend)
            {
                OnCorporationGameServerActionReceived(new ActionEventArgs(action));
            }

            // Deliver any game state request made by the corporation.
            if (mCorporationRequestedGameState)
            {
                mCorporationRequestedGameState = false;
                OnCorporationGameServerGameStateRequested(new EventArgs());
            }

            // Deliever any actions from the hosted game to the corporation.
            List<ActionBase> actionsToDeliver = new List<ActionBase>(mActionsToDelieverToCorporation);
            mActionsToDelieverToCorporation.Clear();
            foreach (ActionBase action in actionsToDeliver)
            {
                OnCorporationGameClientActionReceived(new ActionEventArgs(action));
            }

            // Deliver any game state requests to the corporation.
            List<CorporationGameState> gameStates = new List<CorporationGameState>(mGameStateToDelieverToCorporation);
            mGameStateToDelieverToCorporation.Clear();
            foreach (CorporationGameState gameState in gameStates)
            {
                OnCorporationGameClientGameStateReceived(new CorporationGameStateEventArgs(gameState));
            }
        }

        #endregion

        #region ICorporationGameServer

        private event EventHandler<ActionEventArgs> mCorporationGameServerActionReceived;
        event EventHandler<ActionEventArgs> ICorporationConnectorServerSide.ActionReceived
        {
            add { mCorporationGameServerActionReceived += value; }
            remove { mCorporationGameServerActionReceived -= value; }
        }
        protected void OnCorporationGameServerActionReceived(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> temp = mCorporationGameServerActionReceived;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        private event EventHandler mCorporationGameServerGameStateRequested;
        event EventHandler ICorporationConnectorServerSide.GameStateRequested
        {
            add { mCorporationGameServerGameStateRequested += value; }
            remove { mCorporationGameServerGameStateRequested -= value; }
        }
        protected void OnCorporationGameServerGameStateRequested(EventArgs e)
        {
            EventHandler temp = mCorporationGameServerGameStateRequested;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        void ICorporationConnectorServerSide.SendGameState(CorporationGameState corporationGameState)
        {
            mGameStateToDelieverToCorporation.Add(corporationGameState);
        }

        void ICorporationConnectorServerSide.SendAction(ActionBase action)
        {
            mActionsToDelieverToCorporation.Add(action);
        }

        #endregion

        #region ICorporationGameClient

        private event EventHandler<ActionEventArgs> mCorporationGameClientActionReceived;
        event EventHandler<ActionEventArgs> ICorporationConnectorClientSide.ActionReceived
        {
            add { mCorporationGameClientActionReceived += value; }
            remove { mCorporationGameClientActionReceived -= value; }
        }
        protected void OnCorporationGameClientActionReceived(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> temp = mCorporationGameClientActionReceived;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        private EventHandler<CorporationGameStateEventArgs> mCorporationGameClientGameStateReceived;
        event EventHandler<CorporationGameStateEventArgs> ICorporationConnectorClientSide.GameStateReceived
        {
            add { mCorporationGameClientGameStateReceived += value; }
            remove { mCorporationGameClientGameStateReceived -= value; }
        }
        protected void OnCorporationGameClientGameStateReceived(CorporationGameStateEventArgs e)
        {
            EventHandler<CorporationGameStateEventArgs> temp = mCorporationGameClientGameStateReceived;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        void ICorporationConnectorClientSide.SendAction(ActionBase action)
        {
            mCorporationActionsToSendToHostedGame.Add(action);
        }

        void ICorporationConnectorClientSide.RequestGameState()
        {
            mCorporationRequestedGameState = true;
        }

        #endregion
    }
}
