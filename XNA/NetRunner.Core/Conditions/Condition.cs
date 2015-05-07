using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Conditions
{
    public abstract class Condition
    {
        public virtual void Resolve(GameContext context) { }

        public abstract ConditionStatus IsActive(GameContext context);
    }
}
