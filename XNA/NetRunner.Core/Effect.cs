using NetRunner.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    public abstract class Effect
    {
        public List<Condition> Conditions;

        public Effect()
        {
            Conditions = new List<Condition>();
        }

        public virtual void Resolve(GameContext context)
        {
            foreach (Condition condition in Conditions)
            {
                condition.Resolve(context);
            }
        }

        public abstract void Execute(GameContext context);

        public virtual bool IsActive(GameContext context)
        {
            return Conditions.All(c => c.IsActive(context));
        }
    }
}
