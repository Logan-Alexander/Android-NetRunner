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

        public override bool IsActive(GameContext context)
        {
            return context.CurrentRun == mRun;
        }
    }
}
