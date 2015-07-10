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
    public class InMemoryGameConnector :
        IClient,
        ICorporationConnectorServerSide,
        ICorporationConnectorClientSide,
        IRunnerConnectorServerSide,
        IRunnerConnectorClientSide
    {
        private List<ActionBase> mCorporationActionsToSendToHostedGame = new List<ActionBase>();
        private List<ActionBase> mActionsToDelieverToCorporation = new List<ActionBase>();
        private bool mCorporationRequestedGameState;
        private List<CorporationGameState> mGameStateToDelieverToCorporation = new List<CorporationGameState>();

        private List<ActionBase> mRunnerActionsToSendToHostedGame = new List<ActionBase>();
        private List<ActionBase> mActionsToDelieverToRunner = new List<ActionBase>();
        private bool mRunnerRequestedGameState;
        private List<RunnerGameState> mGameStateToDelieverToRunner = new List<RunnerGameState>();

        #region IClient

        public void Update()
        {
            UpdateCorporation();
            UpdateRunner();
        }

        private void UpdateCorporation()
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

        private void UpdateRunner()
        {
            // Deliver runner actions to hosted game.
            List<ActionBase> actionsToSend = new List<ActionBase>(mRunnerActionsToSendToHostedGame);
            mRunnerActionsToSendToHostedGame.Clear();
            foreach (ActionBase action in actionsToSend)
            {
                OnRunnerGameServerActionReceived(new ActionEventArgs(action));
            }

            // Deliver any game state request made by the runner.
            if (mRunnerRequestedGameState)
            {
                mRunnerRequestedGameState = false;
                OnRunnerGameServerGameStateRequested(new EventArgs());
            }

            // Deliever any actions from the hosted game to the runner.
            List<ActionBase> actionsToDeliver = new List<ActionBase>(mActionsToDelieverToRunner);
            mActionsToDelieverToRunner.Clear();
            foreach (ActionBase action in actionsToDeliver)
            {
                OnRunnerGameClientActionReceived(new ActionEventArgs(action));
            }

            // Deliver any game state requests to the runner.
            List<RunnerGameState> gameStates = new List<RunnerGameState>(mGameStateToDelieverToRunner);
            mGameStateToDelieverToRunner.Clear();
            foreach (RunnerGameState gameState in gameStates)
            {
                OnRunnerGameClientGameStateReceived(new RunnerGameStateEventArgs(gameState));
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

        #region IRunnerGameServer

        private event EventHandler<ActionEventArgs> mRunnerGameServerActionReceived;
        event EventHandler<ActionEventArgs> IRunnerConnectorServerSide.ActionReceived
        {
            add { mRunnerGameServerActionReceived += value; }
            remove { mRunnerGameServerActionReceived -= value; }
        }
        protected void OnRunnerGameServerActionReceived(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> temp = mRunnerGameServerActionReceived;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        private event EventHandler mRunnerGameServerGameStateRequested;
        event EventHandler IRunnerConnectorServerSide.GameStateRequested
        {
            add { mRunnerGameServerGameStateRequested += value; }
            remove { mRunnerGameServerGameStateRequested -= value; }
        }
        protected void OnRunnerGameServerGameStateRequested(EventArgs e)
        {
            EventHandler temp = mRunnerGameServerGameStateRequested;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        void IRunnerConnectorServerSide.SendGameState(RunnerGameState runnerGameState)
        {
            mGameStateToDelieverToRunner.Add(runnerGameState);
        }

        void IRunnerConnectorServerSide.SendAction(ActionBase action)
        {
            mActionsToDelieverToRunner.Add(action);
        }

        #endregion

        #region IRunnerGameClient

        private event EventHandler<ActionEventArgs> mRunnerGameClientActionReceived;
        event EventHandler<ActionEventArgs> IRunnerConnectorClientSide.ActionReceived
        {
            add { mRunnerGameClientActionReceived += value; }
            remove { mRunnerGameClientActionReceived -= value; }
        }
        protected void OnRunnerGameClientActionReceived(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> temp = mRunnerGameClientActionReceived;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        private EventHandler<RunnerGameStateEventArgs> mRunnerGameClientGameStateReceived;
        event EventHandler<RunnerGameStateEventArgs> IRunnerConnectorClientSide.GameStateReceived
        {
            add { mRunnerGameClientGameStateReceived += value; }
            remove { mRunnerGameClientGameStateReceived -= value; }
        }
        protected void OnRunnerGameClientGameStateReceived(RunnerGameStateEventArgs e)
        {
            EventHandler<RunnerGameStateEventArgs> temp = mRunnerGameClientGameStateReceived;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        void IRunnerConnectorClientSide.SendAction(ActionBase action)
        {
            mRunnerActionsToSendToHostedGame.Add(action);
        }

        void IRunnerConnectorClientSide.RequestGameState()
        {
            mRunnerRequestedGameState = true;
        }

        #endregion
    }
}
