using NetRunner.Core.Corporation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Selectors
{
    public class IceProtectingServer : ISelector<PieceOfIce>
    {
        public ISelector<Server> mServerSelector { get; private set; }

        public IceProtectingServer(ISelector<Server> serverSelector)
        {
            mServerSelector = serverSelector;
        }

        public void Resolve(GameContext context)
        {
        }

        public bool IsResolved
        {
            get { return mServerSelector.IsResolved; }
        }

        public IEnumerable<PieceOfIce> Items
        {
            get
            {
                if (IsResolved)
                {
                    return mServerSelector.Items.SelectMany(s => s.Ice);
                }
                else
                {
                    return Enumerable.Empty<PieceOfIce>();
                }
            }
        }
    }
}
