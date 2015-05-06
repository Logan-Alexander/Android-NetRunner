using NetRunner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetRunner.UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DoStuff();
            System.Console.ReadKey();
        }

        static void DoStuff()
        {
            CardFactory cardFactory = new CardFactory();

            GameContext context = new GameContext();
            context.CurrentRun = new Run();

            // Set up a piece of "Chum" Ice.
            Ice ice = (Ice)cardFactory.Create("Chum");
            context.OnIceEncounterStarted(new IceEventArgs(context, ice));
            ice.SubRoutines[0].Execute(context); // Let the subroutine execute.
            context.OnIceEncounterEnded(new IceEventArgs(context, ice));

            // Set up another piece of ice (in the same run) to be affected by the 1st Chum.
            Ice ice2 = (Ice)cardFactory.Create("Chum");
            context.OnIceEncounterStarted(new IceEventArgs(context, ice2));
            context.OnIceEncounterEnded(new IceEventArgs(context, ice2));

            // We should see that the 2nd piece of ice had strength +2 and that
            // the runner was given 3 meat damage after the encounter.

            System.Console.WriteLine("OK!");
        }
    }
}
