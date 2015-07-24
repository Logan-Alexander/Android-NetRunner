using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class UpgradeCardBehaviour : CorporationCardBehaviour, IRezableCard, IServerCard
    {
        public int RezCost { get; private set; }
        public Server Server { get; set; }
        public List<Effect> Effects { get; set; }
        
        public UpgradeCardBehaviour(Card card, int influence, CorporationFaction faction, int rezCost)
            : base(card, influence, faction)
        {
            RezCost = rezCost;
            Effects = new List<Effect>();
        }
    }
}
