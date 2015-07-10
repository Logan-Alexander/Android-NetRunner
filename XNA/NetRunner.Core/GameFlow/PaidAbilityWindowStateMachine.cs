using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public class PaidAbilityWindowStateMachine : StateMachineBase
    {
        public enum StateName
        {
            Corporation,
            CorporationUsingPaidAbility,
            CorporationRezzingNonIce,
            CorporationScoringAgenda,
            Runner,
            RunnerUsingPaidAbility,
            Complete
        }

        public StateName State { get; private set; }
        private StateMachine<StateName, Trigger> _Machine;
        StateMachine<StateName, Trigger>.TriggerWithParameters<object> _ScoreAgendaTrigger;

        public PaidAbilityWindowOptions Options { get; private set; }
        public bool OpponentWillHaveChanceToRespond { get; private set; }

        public PaidAbilityWindowStateMachine(
            Flow stack,
            PlayerType firstToAct,
            PaidAbilityWindowOptions options)
            : base(stack)
        {
            switch (firstToAct)
            {
                case PlayerType.Corporation:
                    State = StateName.Corporation;
                    break;

                case PlayerType.Runner:
                    State = StateName.Runner;
                    break;

                default:
                    throw new NotSupportedException();
            }

            OpponentWillHaveChanceToRespond = true;
            Options = options;
            CreateStateMachine();
        }

        public PaidAbilityWindowStateMachine(
            Flow stack,
            StateName state,
            bool opponentWillHaveChanceToRespond,
            PaidAbilityWindowOptions options)
            : base(stack)
        {
            State = state;
            OpponentWillHaveChanceToRespond = opponentWillHaveChanceToRespond;
            Options = options;
            CreateStateMachine();
        }

        private void CreateStateMachine()
        {
            _Machine = new StateMachine<StateName, Trigger>(() => State, s => State = s);

            _ScoreAgendaTrigger = _Machine.SetTriggerParameters<object>(Trigger.CorporationScoresAgenda);

            _Machine.Configure(StateName.Corporation)
                .PermitDynamic(Trigger.CorporationPasses,
                    () => OpponentWillHaveChanceToRespond
                        ? StateName.Runner
                        : StateName.Complete)
                .OnExit(() => OpponentWillHaveChanceToRespond = false);

            _Machine.Configure(StateName.Runner)
                .PermitDynamic(Trigger.RunnerPasses,
                    () => OpponentWillHaveChanceToRespond
                        ? StateName.Corporation
                        : StateName.Complete)
                .OnExit(() => OpponentWillHaveChanceToRespond = false);

            _Machine.Configure(StateName.Complete)
                .OnEntry(() => GameFlow.Complete());

            if (Options.HasFlag(PaidAbilityWindowOptions.UsePaidAbilities))
            {
                _Machine.Configure(StateName.Corporation)
                    .Permit(Trigger.CorporationUsesPaidAbility, StateName.CorporationUsingPaidAbility);

                _Machine.Configure(StateName.CorporationUsingPaidAbility)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntry(() => CreateCorporationUsingPaidAbilityStateMachine())
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Corporation);

                _Machine.Configure(StateName.Runner)
                    .Permit(Trigger.RunnerUsesPaidAbility, StateName.RunnerUsingPaidAbility);

                _Machine.Configure(StateName.RunnerUsingPaidAbility)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntry(() => CreateRunnerUsingPaidAbilityStateMachine())
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Runner);

            }

            if (Options.HasFlag(PaidAbilityWindowOptions.RezNonIce))
            {
                _Machine.Configure(StateName.Corporation)
                    .Permit(Trigger.CorporationRezzesNonIce, StateName.CorporationRezzingNonIce);

                _Machine.Configure(StateName.CorporationRezzingNonIce)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntry(() => CreateCorporationRezzingNonIceStateMachine())
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Corporation);
            }

            if (Options.HasFlag(PaidAbilityWindowOptions.ScoreAgendas))
            {
                _Machine.Configure(StateName.Corporation)
                    .Permit(Trigger.CorporationScoresAgenda, StateName.CorporationScoringAgenda);

                _Machine.Configure(StateName.CorporationScoringAgenda)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntryFrom(_ScoreAgendaTrigger, cardIdentifier => CreateCorporationScoringAgendaStateMachine(cardIdentifier))
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Corporation);
            }
        }

        private void CreateCorporationUsingPaidAbilityStateMachine()
        {
            // TODO:
            ContinueAfterChildCompletes();
        }

        private void CreateRunnerUsingPaidAbilityStateMachine()
        {
            // TODO:
            ContinueAfterChildCompletes();
        }

        private void CreateCorporationRezzingNonIceStateMachine()
        {
            // TODO:
            ContinueAfterChildCompletes();
        }

        private void CreateCorporationScoringAgendaStateMachine(object cardIdentifier)
        {
            // TODO:
            ContinueAfterChildCompletes();
        }

        internal override void ContinueAfterChildCompletes()
        {
            Fire(Trigger.ChildStateMachineComplete);
        }

        public override void Fire(Trigger trigger)
        {
            _Machine.Fire(trigger);
        }

        public void CorporationScoresAgenda(object cardIdentifer)
        {
            _Machine.Fire(_ScoreAgendaTrigger, cardIdentifer);
        }

        public override bool CanFire(Trigger trigger)
        {
            return _Machine.CanFire(trigger);
        }

        public override string Description
        {
            get
            {
                switch (State)
                {
                    case StateName.Corporation:
                        return "Waiting for Corporation";

                    case StateName.CorporationUsingPaidAbility:
                        return "Corporation Using Paid Ability";

                    case StateName.CorporationRezzingNonIce:
                        return "Corporation Rezzing Non-Ice";

                    case StateName.CorporationScoringAgenda:
                        return "Corporation Scoring Agenda";

                    case StateName.Runner:
                        return "Waiting for Runner";

                    case StateName.RunnerUsingPaidAbility:
                        return "Runner Using Paid Ability";

                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}