using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Conditions
{
    public class Not : Condition
    {
        private Condition mCondition;

        public Not(Condition condition)
        {
            mCondition = condition;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            mCondition.Resolve(context);
        }

        public override bool IsActive(GameContext context)
        {
            return !mCondition.IsActive(context);
        }
    }
}
