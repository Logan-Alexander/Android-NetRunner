using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class CorporationCardBehaviour : CardBehaviour
    {
        public CorporationFaction Faction { get; private set; }

        public CorporationCardBehaviour(
            Card card,
            int influence,
            CorporationFaction faction)
            : base(
                card,
                PlayerType.Corporation,
                influence)
        {
            Faction = faction;
        }
    }
}
