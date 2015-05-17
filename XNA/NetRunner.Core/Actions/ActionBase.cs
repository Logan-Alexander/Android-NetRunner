using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    /// <summary>
    /// Defines an action that can be taken by the player.
    /// </summary>
    [Serializable]
    public abstract class ActionBase
    {
        public abstract bool IsValid(GameContext context, StateMachine stateMachine);

        public abstract void Apply(GameContext context, StateMachine stateMachine);

        protected abstract bool Equals(ActionBase otherAction);

        public override bool Equals(object obj)
        {
            if (obj is ActionBase)
            {
                return Equals((ActionBase)obj);
            }
            else
            {
                return false;
            }
        }
    }
}
