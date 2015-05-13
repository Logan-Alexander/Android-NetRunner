using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class Upgrade : CorporationCard, IRezableCard, IServerCard
    {
        public int RezCost { get; private set; }
        public Server Server { get; set; }
        public List<Effect> Effects { get; set; }
        
        public Upgrade(int id, string title, int influence, CorporationFaction faction, int rezCost)
            : base(id, title, influence, faction)
        {
            RezCost = rezCost;
            Effects = new List<Effect>();
        }
    }
}
