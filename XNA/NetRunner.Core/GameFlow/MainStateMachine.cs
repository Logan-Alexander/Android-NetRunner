using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    /// <summary>
    /// Represents the top-most level of flow. This state machine handles the game setup,
    /// (deck building and initial hand mulligans) and then transitions to InGame.
    /// </summary>
    public class MainStateMachine : StateMachineBase
    {
        public enum StateName
        {
            Setup,
            //TODO: Add options for deck building and mulligans
            InGame
        }

        public StateName State { get; private set; }
        private StateMachine<StateName, Trigger> _Machine;

        public MainStateMachine(Flow stack)
            : this(stack, StateName.Setup)
        {
        }

        public MainStateMachine(Flow stack, StateName state)
            : base(stack)
        {
            State = state;
            CreateStateMachine();
        }

        private void CreateStateMachine()
        {
            _Machine = new StateMachine<StateName, Trigger>(() => State, s => State = s);

            _Machine.Configure(StateName.Setup)
                .Permit(Trigger.GameStarts, StateName.InGame);

            _Machine.Configure(StateName.InGame)
                .OnEntry(() => CreateTurnOrderStateMachine());
        }

        internal override void ContinueAfterChildCompletes()
        {
            // Nothing to do here as the TurnOrder state machine never ends gracefully,
            // it only ends abruptly when the game is over.
        }

        private void CreateTurnOrderStateMachine()
        {
            GameFlow.Add(new TurnOrderStateMachine(GameFlow));
        }

        public override void Fire(Trigger trigger)
        {
            _Machine.Fire(trigger);
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
                    case StateName.Setup:
                        return "Setup";

                    case StateName.InGame:
                        return "In Game";

                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
