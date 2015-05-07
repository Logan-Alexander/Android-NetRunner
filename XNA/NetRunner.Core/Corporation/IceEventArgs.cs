using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    [Serializable]
    public class IceEventArgs : GameContextEventArgs
    {
        public PieceOfIce Ice { get; private set; }

        public IceEventArgs(GameContext context, PieceOfIce ice)
            : base(context)
        {
            if (ice == null)
            {
                throw new ArgumentNullException("ice");
            }

            Ice = ice;
        }
    }
}
