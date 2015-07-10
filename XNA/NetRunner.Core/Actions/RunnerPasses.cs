using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class RunnerPasses : ActionBase
    {
        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.RunnerPasses);
        }

        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            flow.Fire(Trigger.RunnerPasses);
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new RunnerPasses();
        }
    }
}
