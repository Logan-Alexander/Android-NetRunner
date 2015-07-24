using NetRunner.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class OperationCardBehaviour : CorporationCardBehaviour
    {
        public int Cost { get; private set; }
        public List<Condition> PlayConditions { get; private set; }
        public List<Effect> Effects { get; private set; }

        public OperationCardBehaviour(Card card, int influence, CorporationFaction faction, int cost)
            : base(card, influence, faction)
        {
            Cost = cost;
            PlayConditions = new List<Condition>();
            Effects = new List<Effect>();
        }
    }
}
