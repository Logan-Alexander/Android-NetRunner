using NetRunner.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    /// <summary>
    /// Transports actions made by the Runner to a hosted game.
    /// Reports actions from the hosted game to the Runner.
    /// </summary>
    public interface IRunnerConnectorClientSide : IClient
    {
        event EventHandler<ActionEventArgs> ActionReceived;
        event EventHandler<RunnerGameStateEventArgs> GameStateReceived;

        void SendAction(ActionBase action);
        void RequestGameState();
    }
}
