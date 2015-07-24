using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class RemoteServer : Server
    {
        public Card AssetOrAgenda { get; set; }

        public RemoteServer()
            : base(ServerType.Remote)
        {
        }

        /// <summary>
        /// Returns true if the remote server has no asset or agenda card, no ice and no upgrades.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return AssetOrAgenda == null
                    && Ice.Count == 0
                    && Upgrades.Count == 0;
            }
        }
    }
}
