using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class Server
    {
        public ServerType ServerType { get; private set; }

        public List<Card> Ice { get; private set; }

        public List<Card> Upgrades { get; private set; }

        public Server(ServerType serverType)
        {
            ServerType = serverType;
            Ice = new List<Card>();
            Upgrades = new List<Card>();
        }
    }
}
