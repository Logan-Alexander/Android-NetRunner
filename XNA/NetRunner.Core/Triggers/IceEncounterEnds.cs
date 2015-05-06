using NetRunner.Core.IdentifierPredicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Triggers
{
    public class IceEncounterEnds : PostTrigger
    {
        private SingleItemPredicate<Ice> mIcePredicate;
        
        public IceEncounterEnds(SingleItemPredicate<Ice> icePredicate)
        {
            mIcePredicate = icePredicate;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            mIcePredicate.Resolve(context);
            context.IceEncounterEnded += IceEncounterEnded;
        }

        private void IceEncounterEnded(object sender, IceEventArgs e)
        {
            if (e.Ice == mIcePredicate.GetItem(e.Context))
            {
                Trigger(e.Context);
            }
        }
    }
}
