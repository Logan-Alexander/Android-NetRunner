using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Triggers
{
    public abstract class PostTrigger
    {
        public List<Effect> Effects;

        public virtual void Resolve(GameContext context)
        {
            foreach (Effect effect in Effects)
            {
                effect.Resolve(context);
            }
        }

        public PostTrigger()
        {
            Effects = new List<Effect>();
        }

        protected void Trigger(GameContext context)
        {
            foreach (Effect effect in Effects)
            {
                effect.Execute(context);
            }
        }
    }
}
