using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    /// <summary>
    /// Transports actions made by the Corporation to a hosted game.
    /// Reports actions from the hosted game to the Corporation.
    /// </summary>
    public interface ICorporationConnectorClientSide : IClient
    {
        event EventHandler<ActionEventArgs> ActionReceived;
        event EventHandler<CorporationGameStateEventArgs> GameStateReceived;
        
        void SendAction(ActionBase action);
        void RequestGameState();
    }
}
