using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public class CorporationActionsStateMachine : StateMachineBase<CorporationActionsStateMachine.StateName>
    {
        public enum StateName
        {
            ChoosingAction,
            AfterActionPaidAbilityWindow,
            Complete
        }

        public bool HasClicksRemaining
        {
            get { return GameFlow.Context.CorporationClicks > 0; }
        }

        public CorporationActionsStateMachine(Flow stack)
            : base(stack, StateName.ChoosingAction)
        {
        }

        public CorporationActionsStateMachine(Flow stack, StateName state)
            : base(stack, state)
        {
        }

        public override string Description
        {
            get
            {
                switch (State)
                {
                    case StateName.ChoosingAction:
                        return "Choosing action";

                    case StateName.AfterActionPaidAbilityWindow:
                        return "After action paid ability window";

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        protected override void ConfigureStateMachine(Stateless.StateMachine<CorporationActionsStateMachine.StateName, Trigger> machine)
        {
            machine.Configure(StateName.ChoosingAction)
                .Permit(Trigger.CorporationTakesOneCredit, StateName.AfterActionPaidAbilityWindow);

            machine.Configure(StateName.AfterActionPaidAbilityWindow)
                .OnEntry(() => CreatePaidAbilityWindowStateMachine(
                    PlayerType.Corporation,
                    PaidAbilityWindowOptions.UseRezScore))
                .PermitDynamic(Trigger.ChildStateMachineComplete, () => HasClicksRemaining ? StateName.ChoosingAction : StateName.Complete);

            machine.Configure(StateName.Complete)
                .OnEntry(() => GameFlow.Complete());
        }

        private void CreatePaidAbilityWindowStateMachine(PlayerType firstToAct, PaidAbilityWindowOptions options)
        {
            AddChild(new PaidAbilityWindowStateMachine(GameFlow, firstToAct, options));
        }
    }
}
