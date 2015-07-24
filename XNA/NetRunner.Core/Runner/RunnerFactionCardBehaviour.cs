using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public abstract class RunnerFactionCardBehaviour : RunnerCardBehaviour
    {
        public List<PostTrigger> Triggers { get; private set; }

        public RunnerFactionCardBehaviour(Card card, RunnerFaction faction)
            : base(card, 0, faction)
        {
            Triggers = new List<PostTrigger>();
        }
    }
}
