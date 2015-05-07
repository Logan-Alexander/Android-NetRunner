using NetRunner.Core;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Corporation;
using NetRunner.Core.Intents;
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
            PieceOfIce ice = (PieceOfIce)cardFactory.Create("Chum");
            context.OnIceEncounterStarted(new IceEventArgs(context, ice));

            foreach (Subroutine subroutine in ice.SubRoutines)
            {
                if (!subroutine.IsBroken)
                {
                    subroutine.Execute(context);
                }
            }

            context.OnIceEncounterEnded(new IceEventArgs(context, ice));

            // Set up another piece of ice (in the same run) to be affected by the Chum.
            PieceOfIce ice2 = (PieceOfIce)cardFactory.Create("Wall of Thorns");
            context.OnIceEncounterStarted(new IceEventArgs(context, ice2));

            // The code to check for any modifications to the ice will look something like this...
            ModifyIceIntent intent = new ModifyIceIntent();
            intent.Strength = ice2.Strength;
            System.Console.WriteLine("Base strength: {0}", intent.Strength);
            foreach (ContinuousEffect effect in context.ActiveContinuousEffects)
            {
                effect.ModifyIceIntent(context, ice2, intent);
            }
            System.Console.WriteLine("Modified strength: {0}", intent.Strength);

            context.OnIceEncounterEnded(new IceEventArgs(context, ice2));

            // We should see that the 2nd piece of ice had strength +2 and that
            // the runner was given 3 meat damage after the encounter.
        }
    }
}
