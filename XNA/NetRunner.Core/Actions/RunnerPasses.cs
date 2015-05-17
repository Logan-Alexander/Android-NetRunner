using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class RunnerPasses : ActionBase
    {
        public override bool IsValid(GameContext context, GameFlow.StateMachine stateMachine)
        {
            return stateMachine.CanFire(Trigger.RunnerPasses);
        }

        public override void Apply(GameContext context, GameFlow.StateMachine stateMachine)
        {
            stateMachine.Fire(Trigger.RunnerPasses);
        }

        protected override bool Equals(ActionBase otherAction)
        {
            return (otherAction is RunnerPasses);
        }
    }
}
