using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    public abstract class CorporationCard : Card
    {
        public CorporationFaction Faction { get; private set; }

        public CorporationCard(int id, string title, int influence, CorporationFaction faction)
            : base(id, title, PlayerType.Corporation, influence)
        {
            Faction = faction;
        }
    }
}
