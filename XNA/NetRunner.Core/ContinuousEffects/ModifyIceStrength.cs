using NetRunner.Core.Conditions;
using NetRunner.Core.Corporation;
using NetRunner.Core.Selectors;
using NetRunner.Core.Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.ContinuousEffects
{
    public class ModifyIceStrength : ContinuousEffect
    {
        private ISelector<PieceOfIceCardBehaviour> mIceSelector;
        private int mAmount;

        public ModifyIceStrength(ISelector<PieceOfIceCardBehaviour> iceSelector, int amount)
        {
            mIceSelector = iceSelector;
            mAmount = amount;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            mIceSelector.Resolve(context);
        }

        public override void ModifyIceIntent(GameContext context, PieceOfIceCardBehaviour ice, ModifyIceIntent intent)
        {
            if (mIceSelector.IsResolved && mIceSelector.Items.Contains(ice))
            {
                intent.Strength += mAmount;
            }
        }
    }
}
