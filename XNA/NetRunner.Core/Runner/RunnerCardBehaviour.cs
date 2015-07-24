using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public abstract class RunnerCardBehaviour : CardBehaviour
    {
        public RunnerFaction Faction { get; private set; }

        public RunnerCardBehaviour(Card card, int influence, RunnerFaction faction)
            : base(card, PlayerType.Runner, influence)
        {
            Faction = faction;

            if (faction == RunnerFaction.None && influence != 0)
            {
                throw new InvalidOperationException("bad card setup!");
            }
        }
    }
}
