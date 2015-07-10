using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationPasses : ActionBase
    {
        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.CorporationPasses);
        }

        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            flow.Fire(Trigger.CorporationPasses);
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationPasses();
        }
    }
}
