using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public class CorporationDiscardDownToMaxHandSizeStateMachine : StateMachineBase<CorporationDiscardDownToMaxHandSizeStateMachine.StateName>
    {
        public enum StateName
        {
            ChoosingCardToDiscard,
            Complete
        }

        public CorporationDiscardDownToMaxHandSizeStateMachine(Flow stack)
            : base(stack, StateName.ChoosingCardToDiscard)
        {
        }

        public CorporationDiscardDownToMaxHandSizeStateMachine(Flow stack, StateName state)
            : base(stack, state)
        {
        }

        public override string Description
        {
            get
            {
                return "Choosing card to discard";
            }
        }

        protected override void ConfigureStateMachine(Stateless.StateMachine<CorporationDiscardDownToMaxHandSizeStateMachine.StateName, Trigger> machine)
        {
            machine.Configure(StateName.ChoosingCardToDiscard)
                .PermitDynamic(Trigger.CorporationDiscardsCardFromHQ,
                    () => GameFlow.Context.HeadQuarters.Hand.Count > 5
                        ? StateName.ChoosingCardToDiscard
                        : StateName.Complete);

            machine.Configure(StateName.Complete)
                .OnEntry(() => GameFlow.Complete());
        }
    }
}
