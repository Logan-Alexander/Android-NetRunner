using NetRunner.Core.Corporation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Selectors
{
    public class ThisCardsServer : ISelector<Server>
    {
        public IServerCard ServerCard { get; private set; }

        public ThisCardsServer(IServerCard serverCard)
        {
            ServerCard = serverCard;
        }

        public void Resolve(GameContext context)
        {
        }

        public bool IsResolved
        {
            get { return ServerCard.Server != null; }
        }

        public IEnumerable<Server> Items
        {
            get
            {
                if (IsResolved)
                {
                    yield return ServerCard.Server;
                }
            }
        }
    }
}
