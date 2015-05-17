using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    [Serializable]
    public class ActionEventArgs : EventArgs
    {
        public ActionBase Action { get; private set; }

        public ActionEventArgs(ActionBase action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Action = action;
        }
    }
}
