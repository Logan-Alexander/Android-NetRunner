using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    /// <summary>
    /// At many points in the turn order, the players have the opportunity to use paid abiliites,
    /// rez cards or score agendas. Starting with the current player, this process is a
    /// "parliament" where every set of actions allows the opponent to proivde counter-actions.
    /// The parliament ends when both players have had the opportunity to act and have
    /// consecutively passed.
    /// </summary>
    public class PaidAbilityWindowStateMachine : StateMachineBase<PaidAbilityWindowStateMachine.StateName>
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

        StateMachine<StateName, Trigger>.TriggerWithParameters<object> _ScoreAgendaTrigger;

        public PaidAbilityWindowOptions Options { get; private set; }
        public bool OpponentWillHaveChanceToRespond { get; private set; }

        public PaidAbilityWindowStateMachine(
            Flow stack,
            PlayerType firstToAct,
            PaidAbilityWindowOptions options)
            : base(stack, InitialState(firstToAct))
        {
            OpponentWillHaveChanceToRespond = true;
            Options = options;
        }

        public PaidAbilityWindowStateMachine(
            Flow stack,
            StateName state,
            bool opponentWillHaveChanceToRespond,
            PaidAbilityWindowOptions options)
            : base(stack, state)
        {
            OpponentWillHaveChanceToRespond = opponentWillHaveChanceToRespond;
            Options = options;
        }

        private static StateName InitialState(PlayerType firstToAct)
        {
            switch (firstToAct)
            {
                case PlayerType.Corporation:
                    return StateName.Corporation;

                case PlayerType.Runner:
                    return StateName.Runner;

                default:
                    throw new NotSupportedException();
            }
        }

        protected override void ConfigureStateMachine(StateMachine<StateName, Trigger> machine)
        {
            _ScoreAgendaTrigger = machine.SetTriggerParameters<object>(Trigger.CorporationScoresAgenda);
            

            machine.Configure(StateName.Corporation)
                .PermitDynamic(Trigger.CorporationPasses,
                    () => OpponentWillHaveChanceToRespond
                        ? StateName.Runner
                        : StateName.Complete)
                .OnExit(() => OpponentWillHaveChanceToRespond = false);

            machine.Configure(StateName.Runner)
                .PermitDynamic(Trigger.RunnerPasses,
                    () => OpponentWillHaveChanceToRespond
                        ? StateName.Corporation
                        : StateName.Complete)
                .OnExit(() => OpponentWillHaveChanceToRespond = false);

            machine.Configure(StateName.Complete)
                .OnEntry(() => GameFlow.Complete());

            if (Options.HasFlag(PaidAbilityWindowOptions.UsePaidAbilities))
            {
                machine.Configure(StateName.Corporation)
                    .Permit(Trigger.CorporationUsesPaidAbility, StateName.CorporationUsingPaidAbility);

                machine.Configure(StateName.CorporationUsingPaidAbility)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntry(() => CreateCorporationUsingPaidAbilityStateMachine())
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Corporation);

                machine.Configure(StateName.Runner)
                    .Permit(Trigger.RunnerUsesPaidAbility, StateName.RunnerUsingPaidAbility);

                machine.Configure(StateName.RunnerUsingPaidAbility)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntry(() => CreateRunnerUsingPaidAbilityStateMachine())
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Runner);

            }

            if (Options.HasFlag(PaidAbilityWindowOptions.RezNonIce))
            {
                machine.Configure(StateName.Corporation)
                    .Permit(Trigger.CorporationRezzesNonIce, StateName.CorporationRezzingNonIce);

                machine.Configure(StateName.CorporationRezzingNonIce)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntry(() => CreateCorporationRezzingNonIceStateMachine())
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Corporation);
            }

            if (Options.HasFlag(PaidAbilityWindowOptions.ScoreAgendas))
            {
                machine.Configure(StateName.Corporation)
                    .Permit(Trigger.CorporationScoresAgenda, StateName.CorporationScoringAgenda);

                machine.Configure(StateName.CorporationScoringAgenda)
                    .OnEntry(() => OpponentWillHaveChanceToRespond = true)
                    .OnEntryFrom(_ScoreAgendaTrigger, cardIdentifier => CreateCorporationScoringAgendaStateMachine(cardIdentifier))
                    .Permit(Trigger.ChildStateMachineComplete, StateName.Corporation);
            }
        }

        private void CreateCorporationUsingPaidAbilityStateMachine()
        {
            // TODO:
            Fire(Trigger.ChildStateMachineComplete);
        }

        private void CreateRunnerUsingPaidAbilityStateMachine()
        {
            // TODO:
            Fire(Trigger.ChildStateMachineComplete);
        }

        private void CreateCorporationRezzingNonIceStateMachine()
        {
            // TODO:
            Fire(Trigger.ChildStateMachineComplete);
        }

        private void CreateCorporationScoringAgendaStateMachine(object cardIdentifier)
        {
            // TODO:
            Fire(Trigger.ChildStateMachineComplete);
        }

        public override void ScoreAgenda(object cardIdentifier)
        {
            Fire(_ScoreAgendaTrigger, cardIdentifier);
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