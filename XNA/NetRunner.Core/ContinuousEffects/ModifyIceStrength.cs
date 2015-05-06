using NetRunner.Core.Conditions;
using NetRunner.Core.IdentifierPredicates;
using NetRunner.Core.Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.ContinuousEffects
{
    public class ModifyIceStrength : ContinuousEffect
    {
        private SingleItemPredicate<Ice> mIcePredicate;
        private Ice mIce;
        private int mAmount;

        public ModifyIceStrength(SingleItemPredicate<Ice> icePredicate, int amount)
        {
            mIcePredicate = icePredicate;
            mAmount = amount;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            mIcePredicate.Resolve(context);

            mIcePredicate.ItemAcquired += ItemAcquired;
        }

        private void ItemAcquired(object sender, GameContextEventArgs e)
        {
            GameContext context = e.Context;

            mIce = mIcePredicate.GetItem(context);
        }

        public override bool IsActive(GameContext context)
        {
            return base.IsActive(context) && (mIce != null);
        }

        public override void ModifyIceIntent(GameContext context, ModifyIceIntent intent)
        {
            intent.Strength += mAmount;
        }
    }
}
