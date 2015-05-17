using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    /// <summary>
    /// Provides a complete and transportable description of the game from the
    /// Corporation's point of view.
    /// </summary>
    [Serializable]
    public class CorporationGameState
    {
        public State State { get; private set; }
        public bool? OpponentHasHadFirstChanceToRespond { get; private set; }
        public bool? OpponentWillHaveChanceToRespond { get; private set; }

        public CorporationGameState(GameContext gameContext, StateMachine stateMachine)
        {
            // TODO: Create state from context.
            State = stateMachine.State;
            OpponentHasHadFirstChanceToRespond = stateMachine.OpponentHasHadFirstChanceToRespond;
            OpponentWillHaveChanceToRespond = stateMachine.OpponentWillHaveChanceToRespond;
        }
    }
}
