using NetRunner.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class Operation : CorporationCard
    {
        public int Cost { get; private set; }
        public List<Condition> PlayConditions { get; private set; }
        public List<Effect> Effects { get; private set; }

        public Operation(int id, string title, int influence, CorporationFaction faction, int cost)
            : base(id, title, influence, faction)
        {
            Cost = cost;
            PlayConditions = new List<Condition>();
            Effects = new List<Effect>();
        }
    }
}
