using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    [Serializable]
    public class GameContextEventArgs : EventArgs
    {
        public GameContext Context { get; private set; }

        public GameContextEventArgs(GameContext context)
        {
            Context = context;
        }
    }
}
