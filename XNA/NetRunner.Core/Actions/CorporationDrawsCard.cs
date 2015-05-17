using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationDrawsCard : ActionBase
    {
        public override bool IsValid(GameContext context, GameFlow.StateMachine stateMachine)
        {
            return stateMachine.CanFire(Trigger.CorporationCardDrawn);
        }

        public override void Apply(GameContext context, GameFlow.StateMachine stateMachine)
        {
            // TODO: Move the top card of R&D to HQ.
            stateMachine.Fire(Trigger.CorporationCardDrawn);
        }

        protected override bool Equals(ActionBase otherAction)
        {
            return (otherAction is CorporationDrawsCard);
        }
    }
}
