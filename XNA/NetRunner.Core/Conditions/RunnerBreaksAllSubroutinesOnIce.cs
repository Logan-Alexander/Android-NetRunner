using NetRunner.Core.IdentifierPredicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Conditions
{
    public class RunnerBreaksAllSubroutinesOnIce : Condition
    {
        private SingleItemPredicate<Ice> mIcePredicate;
        private Ice mIce;
        private int mBrokenSubRoutineCount;

        public RunnerBreaksAllSubroutinesOnIce(SingleItemPredicate<Ice> icePredicate)
        {
            mIcePredicate = icePredicate;
            mBrokenSubRoutineCount = 0;
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
            context.SubRoutineBroken += SubRoutineBroken;
        }

        private void SubRoutineBroken(object sender, GameContextEventArgs e)
        {
            mBrokenSubRoutineCount++;
        }

        public override bool IsActive(GameContext context)
        {
            return mIce != null && mBrokenSubRoutineCount == mIce.SubRoutines.Count;
        }
    }
}
