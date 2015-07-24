using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class AgendaCardBehaviour : CorporationCardBehaviour, IServerCard
    {
        public int InstallCost { get; private set; }
        public int AdvancementRequirement { get; private set; }
        public int AgendaPoints { get; private set; }

        //TODO: Triggers, effects and paid abilities

        public Server Server { get; set; }

        public AgendaCardBehaviour(Card card, int influence, CorporationFaction faction, int installCost, int advancementRequirement, int agendaPoints)
            : base(card, influence, faction)
        {
            InstallCost = installCost;
            AdvancementRequirement = advancementRequirement;
            AgendaPoints = agendaPoints;
        }
    }
}
