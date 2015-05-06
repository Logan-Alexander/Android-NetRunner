using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.InstantEffects
{
    public abstract class InstantEffect : Effect
    {
        public override void Execute(GameContext context)
        {
            if (IsActive(context))
            {
                ExecuteInstantEffect(context);
            }
        }

        protected abstract void ExecuteInstantEffect(GameContext context);
    }
}
