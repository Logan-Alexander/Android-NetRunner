using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.InstantEffects
{
    public class AddCredits : InstantEffect
    {
        private int mNumberOfCredits;
        private PlayerType mPlayerType;

        public AddCredits(PlayerType playerType, int numberOfCredits)
        {
            mPlayerType = playerType;
            mNumberOfCredits = numberOfCredits;
        }

        protected override void ExecuteInstantEffect(GameContext context)
        {
            switch (mPlayerType)
            {
                case PlayerType.Runner:
                    context.RunnerCredits += mNumberOfCredits;
                    break;

                case PlayerType.Corporation:
                    throw new NotImplementedException();

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
