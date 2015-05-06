using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.IdentifierPredicates
{
    /// <summary>
    /// Identifies the next piece of ice encountered by the Runner.
    /// </summary>
    public class NextPieceOfIce : SingleItemPredicate<Ice>
    {
        private Ice mNextPieceOfIce = null;

        public override void Resolve(GameContext context)
        {
            context.IceEncounterStarted += IceEncounterStarted;
        }

        private void IceEncounterStarted(object sender, IceEventArgs e)
        {
            if (mNextPieceOfIce == null)
            {
                mNextPieceOfIce = e.Ice;
                OnItemAcquired(new GameContextEventArgs(e.Context));
            }
        }

        public override Ice GetItem(GameContext context)
        {
            return mNextPieceOfIce;
        }
    }
}
