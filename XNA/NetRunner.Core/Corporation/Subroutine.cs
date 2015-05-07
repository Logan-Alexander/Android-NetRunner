using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class Subroutine
    {
        public List<Effect> Effects;
        public List<PostTrigger> Triggers;
        public bool IsBroken { get; set; }

        public Subroutine()
        {
            Effects = new List<Effect>();
            Triggers = new List<PostTrigger>();
        }

        public void Execute(GameContext context)
        {
            foreach (Effect effect in Effects)
            {
                effect.Resolve(context);
                effect.Execute(context);
            }

            foreach (PostTrigger trigger in Triggers)
            {
                trigger.Resolve(context);
                // TODO: Add the trigger to the context's list of active triggers.
            }
        }
    }
}
