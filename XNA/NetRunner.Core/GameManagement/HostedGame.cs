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
        public Flow Flow { get; private set; }
        private List<ICorporationConnectorServerSide> mCorporationConnectors;
        private List<IRunnerConnectorServerSide> mRunnerConnectors;

        public HostedGame(GameContext gameContext, Flow flow)
        {
            if (gameContext == null)
            {
                throw new ArgumentNullException("gameContext");
            }

            Context = gameContext;
            Flow = flow;
            mCorporationConnectors = new List<ICorporationConnectorServerSide>();
            mRunnerConnectors = new List<IRunnerConnectorServerSide>();
        }

        public void AddCorporationConnector(ICorporationConnectorServerSide corporationConnector)
        {
            if (corporationConnector == null)
            {
                throw new ArgumentNullException("corporationConnector");
            }

            mCorporationConnectors.Add(corporationConnector);
            corporationConnector.GameStateRequested += CorporationConnector_GameStateRequested;
            corporationConnector.ActionReceived += CorporationConnector_ActionReceived;
        }

        public void AddRunnerConnector(IRunnerConnectorServerSide runnerConnector)
        {
            if (runnerConnector == null)
            {
                throw new ArgumentNullException("runnerConnector");
            }

            mRunnerConnectors.Add(runnerConnector);
            runnerConnector.GameStateRequested += RunnerConnector_GameStateRequested;
            runnerConnector.ActionReceived += RunnerConnector_ActionReceived;
        }

        private void CorporationConnector_GameStateRequested(object sender, EventArgs e)
        {
            ICorporationConnectorServerSide connector = (ICorporationConnectorServerSide)sender;

            SendGameStateToCorporation(connector);
        }

        private void RunnerConnector_GameStateRequested(object sender, EventArgs e)
        {
            IRunnerConnectorServerSide connector = (IRunnerConnectorServerSide)sender;

            SendGameStateToRunner(connector);
        }

        private void SendGameStateToCorporation(ICorporationConnectorServerSide connector)
        {
            CorporationGameState gameState = new CorporationGameState(Context, Flow);

            connector.SendGameState(gameState);
        }

        private void SendGameStateToRunner(IRunnerConnectorServerSide connector)
        {
            RunnerGameState gameState = new RunnerGameState(Context, Flow);

            connector.SendGameState(gameState);
        }

        private void CorporationConnector_ActionReceived(object sender, ActionEventArgs e)
        {
            ActionBase action = e.Action;
            
            // Ensure the action is valid to be taken.
            if (!action.IsValidForServer(Context, Flow))
            {
                // Tell the corporation client to resync.
                ICorporationConnectorServerSide connector = (ICorporationConnectorServerSide)sender;
                SendGameStateToCorporation(connector);
                return;
            }

            // Replay the action to all connectors.
            // This will allow the corporation to confirm that action was received and
            // applied.
            ActionBase responseForCorporation = action.Clone();
            responseForCorporation.AddInformationForCorporation();
            foreach (var corporationConnector in mCorporationConnectors)
            {
                corporationConnector.SendAction(responseForCorporation);
            }
            
            // Send the action to all runner connectors.
            ActionBase responseForRunner = action.Clone();
            responseForRunner.AddInformationForRunner();
            foreach (var runnerConnector in mRunnerConnectors)
            {
                runnerConnector.SendAction(responseForRunner);
            }

            action.ApplyToServer(Context, Flow);
        }

        private void RunnerConnector_ActionReceived(object sender, ActionEventArgs e)
        {
            ActionBase action = e.Action;

            // Ensure the action is valid to be taken.
            if (!action.IsValidForServer(Context, Flow))
            {
                // Tell the runner client to resync.
                IRunnerConnectorServerSide connector = (IRunnerConnectorServerSide)sender;
                SendGameStateToRunner(connector);
                return;
            }

            // Replay the action to all connectors.
            // This will allow the runner to confirm that action was received and
            // applied.
            ActionBase responseForRunner = action.Clone();
            responseForRunner.AddInformationForRunner();
            foreach (var runnerConnector in mRunnerConnectors)
            {
                runnerConnector.SendAction(responseForRunner);
            }

            // Send the action to all runner connectors.
            ActionBase responseForCorporation = action.Clone();
            responseForCorporation.AddInformationForCorporation();
            foreach (var corporationConnector in mCorporationConnectors)
            {
                corporationConnector.SendAction(responseForCorporation);
            }

            action.ApplyToServer(Context, Flow);
        }

    }
}
