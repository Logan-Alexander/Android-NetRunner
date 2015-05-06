using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Conditions
{
    public class Always : Condition
    {
        public override bool IsActive(GameContext context)
        {
            return true;
        }
    }
}
