using NetRunner.Core.Corporation;
using NetRunner.Core.Selectors;
using NetRunner.Core.Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.ContinuousEffects
{
    public class ModifyRezCost : ContinuousEffect
    {
        private ISelector<IRezableCard> mRezableCardsSelector;
        private int mAmount;

        public ModifyRezCost(ISelector<IRezableCard> rezableCardsSelector, int amount)
        {
            mRezableCardsSelector = rezableCardsSelector;
            mAmount = amount;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            mRezableCardsSelector.Resolve(context);
        }

        public override void ModifyRezIntent(GameContext context, IRezableCard rezableCard, ModifyRezIntent intent)
        {
            if (mRezableCardsSelector.IsResolved && mRezableCardsSelector.Items.Contains(rezableCard))
            {
                intent.RezCost += mAmount;
            }
        }
    }
}
