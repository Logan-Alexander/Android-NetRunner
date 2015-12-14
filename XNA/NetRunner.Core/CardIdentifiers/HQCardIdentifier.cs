using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardIdentifiers
{
    public class HQCardIdentifier : CardIdentifier
    {
        public int Index { get; private set; }

        public HQCardIdentifier(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            Index = index;
        }

        public override bool TryResolve(GameContext context, out Card card)
        {
            card = null;
            if (Index > context.HeadQuarters.Hand.Count)
            {
                return false;
            }

            card = Resolve(context);
            return true;
        }

        public override Card Resolve(GameContext context)
        {
            return context.HeadQuarters.Hand[Index];
        }
    }
}
