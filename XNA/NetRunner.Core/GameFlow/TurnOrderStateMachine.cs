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
    public class TurnOrderStateMachine : StateMachineBase
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

        public StateName State { get; private set; }
        private StateMachine<StateName, Trigger> _Machine;

        public TurnOrderStateMachine(Flow stack)
            : this(stack, StateName.Auto)
        {
        }

        public TurnOrderStateMachine(Flow stack, StateName state)
            : base(stack)
        {
            State = state;
            CreateStateMachine();
        }

        private void CreateStateMachine()
        {
            _Machine = new StateMachine<StateName, Trigger>(() => State, s => State = s);

            // We need to use a Auto state here to jump into the first actual state as that
            // state has an OnEntry() action. Without this Auto state, that action would
            // never fire.
            _Machine.Configure(StateName.Auto)
                .Permit(Trigger.Auto, StateName.Corp_1_1_PaidAbilityWindow);

            _Machine.Configure(StateName.Corp_1_1_PaidAbilityWindow)
                .OnEntry(() => CreatePaidAbilityWindowStateMachine(
                    PlayerType.Corporation,
                    PaidAbilityWindowOptions.UseRezScore))
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_1_2_TurnBegins);

            _Machine.Configure(StateName.Corp_1_2_TurnBegins)
                .OnEntry(() => CreateBeginTurnStateMachine(PlayerType.Corporation))
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_1_3_DrawOneCard);

            _Machine.Configure(StateName.Corp_1_3_DrawOneCard)
                .Permit(Trigger.CorporationCardDrawn, StateName.Corp_2_1_PaidAbilityWindow);

            _Machine.Configure(StateName.Corp_2_1_PaidAbilityWindow)
                .OnEntry(() => CreatePaidAbilityWindowStateMachine(
                    PlayerType.Corporation,
                    PaidAbilityWindowOptions.UseRezScore))
                .Permit(Trigger.ChildStateMachineComplete, StateName.Corp_2_2_TakeActions);
        }

        internal override void ContinueAfterChildCompletes()
        {
            Fire(Trigger.ChildStateMachineComplete);
        }

        private void CreatePaidAbilityWindowStateMachine(PlayerType firstToAct, PaidAbilityWindowOptions options)
        {
            GameFlow.Add(new PaidAbilityWindowStateMachine(GameFlow, firstToAct, options));
        }

        private void CreateBeginTurnStateMachine(PlayerType playerType)
        {
            // TODO
            ContinueAfterChildCompletes();
        }

        public override void Fire(Trigger trigger)
        {
            _Machine.Fire(trigger);
        }

        public override bool CanFire(Trigger trigger)
        {
            return _Machine.CanFire(trigger);
        }

        internal override void Start()
        {
            Fire(Trigger.Auto);
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

        public override IEnumerable<Trigger> PermittedTriggers
        {
            get { return _Machine.PermittedTriggers; }
        }
    }
}
