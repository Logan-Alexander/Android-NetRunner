using NetRunner.Core.Corporation;
using NetRunner.Core.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Conditions
{
    public class RunnerBreaksAllSubroutinesOnIce : Condition
    {
        private ISelector<PieceOfIceCardBehaviour> mIceSelector;

        public RunnerBreaksAllSubroutinesOnIce(ISelector<PieceOfIceCardBehaviour> iceSelector)
        {
            mIceSelector = iceSelector;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            mIceSelector.Resolve(context);
        }

        public override ConditionStatus IsActive(GameContext context)
        {
            if (!mIceSelector.IsResolved)
            {
                return ConditionStatus.NotApplicable;
            }

            if (mIceSelector.Items.All(i => i.SubRoutines.All(s => s.IsBroken)))
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
