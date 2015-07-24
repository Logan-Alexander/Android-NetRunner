using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class AssetCardBehaviour : CorporationCardBehaviour, IServerCard
    {
        public int InstallCost { get; private set; }
        public bool Advancable { get; private set; }

        //TODO: Triggers, effects and paid abilities

        public Server Server { get; set; }

        public AssetCardBehaviour(Card card, int influence, CorporationFaction faction, int installCost, bool advancable)
            : base(card, influence, faction)
        {
            InstallCost = installCost;
            Advancable = advancable;
        }
    }
}
