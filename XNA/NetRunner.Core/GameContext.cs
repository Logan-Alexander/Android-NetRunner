using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    public class GameContext
    {
        public event EventHandler<IceEventArgs> IceEncounterStarted;
        public void OnIceEncounterStarted(IceEventArgs e)
        {
            EventHandler<IceEventArgs> temp = IceEncounterStarted;
            if (temp != null)
            {
                temp(this, e);
            }
        }
        
        public event EventHandler<GameContextEventArgs> SubRoutineBroken;

        public event EventHandler<IceEventArgs> IceEncounterEnded;
        public void OnIceEncounterEnded(IceEventArgs e)
        {
            EventHandler<IceEventArgs> temp = IceEncounterEnded;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        public int RunnerCredits { get; set; }
        public Run CurrentRun { get; set; }
    }
}
