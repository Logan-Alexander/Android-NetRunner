using NetRunner.Core.Corporation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Selectors
{
    public class ThisCardsServer : ISelector<Server>
    {
        public Card Card { get; private set; }

        public ThisCardsServer(Card card)
        {
            Card = card;
        }

        public void Resolve(GameContext context)
        {
        }

        public bool IsResolved
        {
            get
            {
                IServerCard serverCard = Card.Behaviour as IServerCard;
                return serverCard != null && serverCard.Server != null;
            }
        }

        public IEnumerable<Server> Items
        {
            get
            {
                if (IsResolved)
                {
                    IServerCard serverCard = (IServerCard)Card.Behaviour;
                    yield return serverCard.Server;
                }
            }
        }
    }
}
