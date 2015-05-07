using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Conditions
{
    public class CurrentRun : Condition
    {
        private Run mRun;

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);

            if (context.CurrentRun == null)
            {
                throw new InvalidOperationException();
            }
            
            mRun = context.CurrentRun;
        }

        public override ConditionStatus IsActive(GameContext context)
        {
            if (context.CurrentRun == null)
            {
                return ConditionStatus.NotApplicable;
            }
            else if (context.CurrentRun == mRun)
            {
                return ConditionStatus.Active;
            }
            else
            {
                return ConditionStatus.Inactive;
            }
        }
    }
}
