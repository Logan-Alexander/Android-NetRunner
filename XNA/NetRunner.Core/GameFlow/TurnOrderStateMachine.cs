using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    /// <summary>
    /// This flow defines the process for taking turns.
    /// </summary>
    public class TurnOrderStateMachine : StateMachineBase<TurnOrderStateMachine.StateName>
    {
        public enum StateName
        {
            Auto,
            Corp_1_1_PaidAbilityWindow,
            Corp_1_2_TurnBegins,
            Corp_1_3_DrawOneCard,
            Corp_2_1_PaidAbilityWindow,
            Corp_2_2_TakeActions,
            Corp_3_1_DiscardDownToMaxHandSize,
            Corp_3_2_PaidAbilityWindow,
            Corp_3_3_EndOfTurn,
            Runner_1_1_PaidAbilityWindow,
        }

        public TurnOrderStateMachine(Flow stack)
            : this(stack, StateName.Auto)
        {
        }

        public TurnOrderStateMachine(Flow stack, StateName state)
            : base(stack, state)
        {
        }

        protected override void ConfigureStateMachine(StateMachine<StateName, Trigger> machine)
        {
            // We need to use a Auto state here to jump into the first actual state as that
            // state has an OnEntry() action. Without this Auto state, that action would
            // never fire.
            machine.Configure(StateName.Auto)
                .Permit(Trigger.Auto, StateName.Corp_1_1_PaidAbilityWindow);

            machine.Configure(StateName.Corp_1_1_PaidAbilityWindow)
                .OnEntry(() => GameFlow.Context.CorporationClicks = 3)
                .OnEntry(() => CreatePaidAbilityWindowStateMachine(
                    PlayerType.Corporation,
                    PaidAbilityWindowOptions.UseRezScore))
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_1_2_TurnBegins);

            machine.Configure(StateName.Corp_1_2_TurnBegins)
                .OnEntry(() => CreateBeginTurnStateMachine(
                    PlayerType.Corporation))
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_1_3_DrawOneCard);

            machine.Configure(StateName.Corp_1_3_DrawOneCard)
                .Permit(Trigger.CorporationDrawsCardAtStartOfTurn, StateName.Corp_2_1_PaidAbilityWindow);

            machine.Configure(StateName.Corp_2_1_PaidAbilityWindow)
                .OnEntry(() => CreatePaidAbilityWindowStateMachine(
                    PlayerType.Corporation,
                    PaidAbilityWindowOptions.UseRezScore))
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_2_2_TakeActions);

            machine.Configure(StateName.Corp_2_2_TakeActions)
                .OnEntry(() => CreateCorporationActionsStateMachine())
                .PermitDynamic(Trigger.ChildStateMachineComplete,
                    () => GameFlow.Context.HeadQuarters.Hand.Count > GameFlow.Context.CorporationHandLimit
                        ? StateName.Corp_3_1_DiscardDownToMaxHandSize
                        : StateName.Corp_3_2_PaidAbilityWindow);

            machine.Configure(StateName.Corp_3_1_DiscardDownToMaxHandSize)
                .OnEntry(() => CreateCorporationDiscardDownToMaxHandSizeStateMachine())
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_3_2_PaidAbilityWindow);

            machine.Configure(StateName.Corp_3_2_PaidAbilityWindow)
                .OnEntry(() => CreatePaidAbilityWindowStateMachine(
                    PlayerType.Corporation,
                    PaidAbilityWindowOptions.UsePaidAbilities | PaidAbilityWindowOptions.RezNonIce))
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_3_3_EndOfTurn);

        }

        private void CreatePaidAbilityWindowStateMachine(PlayerType firstToAct, PaidAbilityWindowOptions options)
        {
            AddChild(new PaidAbilityWindowStateMachine(GameFlow, firstToAct, options));
        }

        private void CreateCorporationActionsStateMachine()
        {
            AddChild(new CorporationActionsStateMachine(GameFlow));
        }

        private void CreateBeginTurnStateMachine(PlayerType playerType)
        {
            // TODO: Create a state machine that handles "When your turn begins..." conditionals for the corporation.
            Fire(Trigger.ChildStateMachineComplete);
        }

        private void CreateCorporationDiscardDownToMaxHandSizeStateMachine()
        {
            AddChild(new CorporationDiscardDownToMaxHandSizeStateMachine(GameFlow));
        }

        public override string Description
        {
            get
            {
                switch (State)
                {
                    case StateName.Corp_1_1_PaidAbilityWindow:
                        return "Corporation Draw Phase: 1.1 Paid Ability Window";

                    case StateName.Corp_1_2_TurnBegins:
                        return "Corporation Draw Phase: 1.2 Turn Begins";

                    case StateName.Corp_1_3_DrawOneCard:
                        return "Corporation Draw Phase: 1.3 Draw One Card";

                    case StateName.Corp_2_1_PaidAbilityWindow:
                        return "Corporation Action Phase: 2.1 Paid Ability Window";

                    case StateName.Corp_2_2_TakeActions:
                        return "Corporation Action Phase: 2.2 Take Actions";

                    case StateName.Corp_3_1_DiscardDownToMaxHandSize:
                        return "Corporation Discard Phase: 3.1 Discard Down To Maximum Hand Size";

                    case StateName.Corp_3_2_PaidAbilityWindow:
                        return "Corporation Discard Phase: 3.2 Paid Ability Window";

                    case StateName.Corp_3_3_EndOfTurn:
                        return "Corporation Discard Phase: 3.3 Turn Ends";

                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}
