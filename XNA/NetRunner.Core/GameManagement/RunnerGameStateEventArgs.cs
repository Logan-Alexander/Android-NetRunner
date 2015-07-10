using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    [Serializable]
    public class RunnerGameStateEventArgs : EventArgs
    {
        public RunnerGameState RunnerGameState { get; private set; }

        public RunnerGameStateEventArgs(RunnerGameState runnerGameState)
        {
            if (runnerGameState == null)
            {
                throw new ArgumentNullException("runnerGameState");
            }

            RunnerGameState = runnerGameState;
        }
    }
}
