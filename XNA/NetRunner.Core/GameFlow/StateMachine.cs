using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public class StateMachine
    {
        public State State { get; private set; }
        public bool? OpponentWillHaveChanceToRespond { get; private set; }
        public bool? OpponentHasHadFirstChanceToRespond { get; private set; }

        private StateMachine<State, Trigger> _Machine;

        public StateMachine()
            : this(State.Setup, null, null)
        {
        }

        public StateMachine(
            State state,
            bool? opponentWillHaveChanceToRespond,
            bool? opponentHasHadFirstChanceToRespond)
        {
            State = state;
            OpponentWillHaveChanceToRespond = opponentWillHaveChanceToRespond;
            OpponentHasHadFirstChanceToRespond = opponentHasHadFirstChanceToRespond;

            CreateStateMachine();
        }

        private void CreateStateMachine()
        {
            _Machine = new StateMachine<State, Trigger>(() => State, s => State = s);

            _Machine.Configure(State.Setup)
                .Permit(Trigger.GameStarts, State.CorporationTurn);

            _Machine.Configure(State.CorporationTurn)
                .OnEntry(() => _Machine.Fire(Trigger.Auto))
                .Permit(Trigger.Auto, State.CorporationDrawPhase);

            ConfigureCorporationDrawPhase();
        }

        private void ConfigureCorporationDrawPhase()
        {
            _Machine.Configure(State.CorporationDrawPhase)
                .SubstateOf(State.CorporationTurn)
                .OnEntry(() => _Machine.Fire(Trigger.Auto))
                .Permit(Trigger.Auto, State.CorporationDrawPhasePaidAbilityWindow);

            ConfigurePaidAbilityWindow(
                State.CorporationDrawPhase,
                State.CorporationDrawPhasePaidAbilityWindow,
                State.CorporationDrawPhasePaidAbilityWindowCorporation,
                State.CorporationDrawPhasePaidAbilityWindowRunner,
                State.CorporationDrawPhaseDrawingCard,
                PaidAbilityWindowOptions.UseRezScore,
                true);

            _Machine.Configure(State.CorporationDrawPhaseDrawingCard)
                .Permit(Trigger.CorporationCardDrawn, State.CorporationActionPhase);
        }

        private void ConfigurePaidAbilityWindow(
            State superState,
            State window,
            State corporationState,
            State runnerState,
            State nextStateAfterWindowIsComplete,
            PaidAbilityWindowOptions options,
            bool corporationIsFirst)
        {
            State initialState = corporationIsFirst ? corporationState : runnerState;

            _Machine.Configure(window)
                .SubstateOf(superState)
                .OnEntry(() => OpponentHasHadFirstChanceToRespond = false)
                .OnEntry(() => _Machine.Fire(Trigger.Auto))
                .Permit(Trigger.Auto, initialState)
                .OnExit(() => OpponentHasHadFirstChanceToRespond = null);

            _Machine.Configure(corporationState)
                .SubstateOf(window)
                .OnEntry(() => OpponentWillHaveChanceToRespond = false)
                .OnEntryFrom(Trigger.CorporationUsesPaidAbility, () => OpponentWillHaveChanceToRespond = true)
                .OnEntryFrom(Trigger.CorporationRezzesNonIce, () => OpponentWillHaveChanceToRespond = true)
                .OnEntryFrom(Trigger.CorporationScoresAgenda, () => OpponentWillHaveChanceToRespond = true)
                .PermitDynamic(Trigger.CorporationPasses,
                    () => OpponentWillHaveChanceToRespond.Value || !OpponentHasHadFirstChanceToRespond.Value
                        ? runnerState
                        : nextStateAfterWindowIsComplete)
                .OnExit(() => OpponentWillHaveChanceToRespond = null);

            _Machine.Configure(runnerState)
                .SubstateOf(window)
                .OnEntry(() => OpponentHasHadFirstChanceToRespond = true)
                .OnEntry(() => OpponentWillHaveChanceToRespond = false)
                .OnEntryFrom(Trigger.RunnerUsesPaidAbility, () => OpponentWillHaveChanceToRespond = true)
                .PermitDynamic(Trigger.RunnerPasses,
                    () => OpponentWillHaveChanceToRespond.Value
                        ? corporationState
                        : nextStateAfterWindowIsComplete)
                .OnExit(() => OpponentWillHaveChanceToRespond = null);

            if (options.HasFlag(PaidAbilityWindowOptions.UsePaidAbilities))
            {
                _Machine.Configure(corporationState)
                    .PermitReentry(Trigger.CorporationUsesPaidAbility);

                _Machine.Configure(runnerState)
                    .PermitReentry(Trigger.RunnerUsesPaidAbility);
            }

            if (options.HasFlag(PaidAbilityWindowOptions.RezNonIce))
            {
                _Machine.Configure(corporationState)
                    .PermitReentry(Trigger.CorporationRezzesNonIce);
            }

            if (options.HasFlag(PaidAbilityWindowOptions.ScoreAgendas))
            {
                _Machine.Configure(corporationState)
                    .PermitReentry(Trigger.CorporationScoresAgenda);
            }
        }

        public void Fire(Trigger trigger)
        {
            _Machine.Fire(trigger);
        }

        public IEnumerable<Trigger> PermittedTriggers
        {
            get
            {
                return _Machine.PermittedTriggers;
            }
        }

        public bool CanFire(Trigger trigger)
        {
            return _Machine.CanFire(trigger);
        }
    }
}
