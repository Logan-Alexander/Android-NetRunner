using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class Server
    {
        public ServerType ServerType { get; private set; }

        public List<PieceOfIce> Ice { get; private set; }

        public List<Upgrade> Upgrades { get; private set; }

        public Server(ServerType serverType)
        {
            ServerType = serverType;
            Ice = new List<PieceOfIce>();
            Upgrades = new List<Upgrade>();
        }
    }
}
