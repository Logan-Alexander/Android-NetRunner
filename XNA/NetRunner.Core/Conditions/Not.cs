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

        public override ConditionStatus IsActive(GameContext context)
        {
            switch (mCondition.IsActive(context))
            {
                case ConditionStatus.NotApplicable:
                    return ConditionStatus.NotApplicable;

                case ConditionStatus.Active:
                    return ConditionStatus.Inactive;

                case ConditionStatus.Inactive:
                    return ConditionStatus.Active;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
