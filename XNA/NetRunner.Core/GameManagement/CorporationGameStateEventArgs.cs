using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    [Serializable]
    public class CorporationGameStateEventArgs : EventArgs
    {
        public CorporationGameState CorporationGameState { get; private set; }

        public CorporationGameStateEventArgs(CorporationGameState corporationGameState)
        {
            if (corporationGameState == null)
            {
                throw new ArgumentNullException("corporationGameState");
            }

            CorporationGameState = corporationGameState;
        }
    }
}
