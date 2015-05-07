using NetRunner.Core.Corporation;
using NetRunner.Core.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Triggers
{
    public class IceEncounterEnds : PostTrigger
    {
        private ISelector<PieceOfIce> mIceSelector;
        
        public IceEncounterEnds(ISelector<PieceOfIce> iceSelector)
        {
            mIceSelector = iceSelector;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            mIceSelector.Resolve(context);
            context.IceEncounterEnded += IceEncounterEnded;
        }

        private void IceEncounterEnded(object sender, IceEventArgs e)
        {
            if (mIceSelector.Items.Contains(e.Ice))
            {
                Trigger(e.Context);
            }
        }
    }
}
