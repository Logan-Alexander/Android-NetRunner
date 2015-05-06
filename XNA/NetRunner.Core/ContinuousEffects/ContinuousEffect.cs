using NetRunner.Core.Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.ContinuousEffects
{
    public abstract class ContinuousEffect : Effect
    {
        public override void Execute(GameContext context)
        {
            // TODO: Add effect to the context's list of continuous effects.
        }

        public virtual void ModifyIceIntent(GameContext context, ModifyIceIntent intent) { }
    }
}
