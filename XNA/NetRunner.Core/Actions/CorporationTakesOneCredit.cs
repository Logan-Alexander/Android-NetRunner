using NetRunner.Core.CardIdentifiers;
using NetRunner.Core.Corporation;
using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationTakesOneCredit : ActionBase
    {
        public CorporationTakesOneCredit()
        {
        }

        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.CorporationTakesOneCredit);
        }

        protected override bool IsContextValidForCorporation(GameContext context)
        {
            return context.CorporationClicks > 0;
        }

        protected override bool IsContextValidForServer(GameContext context)
        {
            return context.CorporationClicks > 0;
        }

        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            context.CorporationCredits += 1;
            context.CorporationClicks -= 1;
            
            flow.CurrentStateMachine.Fire(Trigger.CorporationTakesOneCredit);
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationTakesOneCredit();
        }
    }
}
