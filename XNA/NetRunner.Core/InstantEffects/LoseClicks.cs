using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.InstantEffects
{
    public class LoseClicks : InstantEffect
    {
        public int NumberOfClicks { get; private set; }

        public LoseClicks(int numberOfClicks)
        {
            NumberOfClicks = numberOfClicks;
        }

        protected override void ExecuteInstantEffect(GameContext context)
        {
            throw new NotImplementedException();
        }
    }
}
