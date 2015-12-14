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
    public class MainStateMachine : StateMachineBase<MainStateMachine.StateName>
    {
        public enum StateName
        {
            Setup,
            //TODO: Add options for deck building and mulligans
            InGame
        }

        public MainStateMachine(Flow stack)
            : this(stack, StateName.Setup)
        {
        }

        public MainStateMachine(Flow stack, StateName state)
            : base(stack, state)
        {
        }

        protected override void ConfigureStateMachine(StateMachine<StateName, Trigger> machine)
        {
            machine.Configure(StateName.Setup)
                .Permit(Trigger.GameStarts, StateName.InGame);

            machine.Configure(StateName.InGame)
                .OnEntry(() => CreateTurnOrderStateMachine());
        }

        private void CreateTurnOrderStateMachine()
        {
            AddChild(new TurnOrderStateMachine(GameFlow));
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
