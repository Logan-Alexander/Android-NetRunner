using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    [Serializable]
    public class IceEventArgs : GameContextEventArgs
    {
        public Ice Ice { get; private set; }

        public IceEventArgs(GameContext context, Ice ice)
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
