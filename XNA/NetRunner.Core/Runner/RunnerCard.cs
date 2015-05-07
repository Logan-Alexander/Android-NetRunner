using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public abstract class RunnerCard : Card
    {
        public RunnerFaction Faction { get; private set; }
        public int Cost { get; private set; }

        public RunnerCard(int id, string title, int influence, RunnerFaction faction, int cost)
            : base(id, title, PlayerType.Runner, influence)
        {
            Faction = faction;
            Cost = cost;

            if (faction == RunnerFaction.None && influence != 0)
            {
                throw new InvalidOperationException("bad card setup!");
            }
        }
    }
}
