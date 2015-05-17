using NetRunner.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    public interface ICorporationConnectorServerSide
    {
        event EventHandler<ActionEventArgs> ActionReceived;
        event EventHandler GameStateRequested;

        void SendGameState(CorporationGameState corporationGameState);
        void SendAction(ActionBase action);
    }
}
