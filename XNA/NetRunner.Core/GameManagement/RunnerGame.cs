using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetRunner.Core.GameFlow;
using NetRunner.Core.Actions;

namespace NetRunner.Core.GameManagement
{
    /// <summary>
    /// A runner game is a game seen from the Runner's point of view.
    /// I.e. The Runner's cards will be visible, but the Runner's cards will not.
    /// Any action that is taken by the runner will be sent to the hosted game.
    /// </summary>
    public class RunnerGame
    {
        public event EventHandler GameOutOfSync;
        protected void OnGameOutOfSync(EventArgs e)
        {
            EventHandler temp = GameOutOfSync;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        private IRunnerConnectorClientSide _Connector;
        public GameContext Context { get; private set; }
        public Flow Flow { get; private set; }
        public bool WaitingForDeferedExection { get; set; }

        private Queue<ActionBase> _UnconfirmedActions = new Queue<ActionBase>();

        public RunnerGame(IRunnerConnectorClientSide connector)
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
                if (unconfirmedAction.HasSameID(e.Action))
                {
                    _UnconfirmedActions.Dequeue();

                    if (e.Action.DeferExecution)
                    {
                        ApplyAction(e.Action, false);
                    }
                }
                else
                {
                    OnGameOutOfSync(new EventArgs());
                    Resync();
                    return;
                }
            }
            else
            {
                ApplyAction(e.Action, false);
            }
        }

        private void Connector_GameStateReceived(object sender, RunnerGameStateEventArgs e)
        {
            Load(e.RunnerGameState);
        }

        private void Load(RunnerGameState runnerGameState)
        {
            GameContextSerializer gameContextSerializer = new GameContextSerializer();
            Context = gameContextSerializer.Deserialize(runnerGameState.SerializedGameContext);

            Flow = new Flow(runnerGameState.SerializedFlow);
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
            if (!action.IsValidForRunner(Context, Flow))
            {
                OnGameOutOfSync(new EventArgs());
                Resync();
                return;
            }

            if (sendToHostedGame)
            {
                _UnconfirmedActions.Enqueue(action);

                // Send the action to the hosted game via our connector.
                _Connector.SendAction(action);
            }

            // We want to see an immediate effect rather than wait for the round-trip to the server.
            if (!action.DeferExecution || !sendToHostedGame)
            {
                action.ApplyToRunner(Context, Flow);
                WaitingForDeferedExection = false;
            }
            else
            {
                WaitingForDeferedExection = true;
            }
        }

        public void Resync()
        {
            _Connector.RequestGameState();
        }
    }
}
