using NetRunner.Core.Corporation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Selectors
{
    /// <summary>
    /// Identifies the next piece of ice encountered by the Runner.
    /// </summary>
    public class NextPieceOfIce : ISelector<PieceOfIceCardBehaviour>
    {
        private PieceOfIceCardBehaviour mNextPieceOfIce = null;

        public void Resolve(GameContext context)
        {
            context.IceEncounterStarted += IceEncounterStarted;
        }

        private void IceEncounterStarted(object sender, IceEventArgs e)
        {
            if (mNextPieceOfIce == null)
            {
                mNextPieceOfIce = e.Ice;
            }
        }

        public bool IsResolved
        {
            get { return mNextPieceOfIce != null; }
        }

        public IEnumerable<PieceOfIceCardBehaviour> Items
        {
            get
            {
                if (IsResolved)
                {
                    yield return mNextPieceOfIce;
                }
            }
        }
    }
}
