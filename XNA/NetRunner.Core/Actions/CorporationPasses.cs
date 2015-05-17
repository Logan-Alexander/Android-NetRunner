using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationPasses : ActionBase
    {
        public override bool IsValid(GameContext context, GameFlow.StateMachine stateMachine)
        {
            return stateMachine.CanFire(Trigger.CorporationPasses);
        }

        public override void Apply(GameContext context, GameFlow.StateMachine stateMachine)
        {
            stateMachine.Fire(Trigger.CorporationPasses);
        }

        protected override bool Equals(ActionBase otherAction)
        {
            return (otherAction is CorporationPasses);
        }
    }
}
