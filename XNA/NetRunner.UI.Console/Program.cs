using NetRunner.Core;
using NetRunner.Core.Actions;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Corporation;
using NetRunner.Core.GameFlow;
using NetRunner.Core.GameManagement;
using NetRunner.Core.GameManagement.InMemory;
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
            //DoStuff();

            // -------- IN AN INTERNET GAME, THIS PART WOULD HAPPEN ON THE WEB SERVER -------- 

            // Create the GameContext. This will hold all information about the game.
            GameContext gameContext = new GameContext();
            StateMachine stateMachine = new StateMachine();
            stateMachine.Fire(Trigger.GameStarts);

            // Create a HostedGame.
            // This will allow information about the game to be broadcast to all players.
            // The information sent to the Runner and the Corporation will be different.
            // For example, when the Corporation draws a card, the Corporation will be told
            // which card was moved from R&D to Headquarters. The Runner will only be told
            // that a card was moved.
            HostedGame hostedGame = new HostedGame(gameContext, stateMachine);

            // An "in-memory" game connector allows information to flow directly from clients
            // to the hosted game. In a local game, this will be sufficient.
            // For an internet game, the web server would create an instance
            // of some connector that implemented the server-side interface.
            InMemoryGameConnector inMemoryConnector = new InMemoryGameConnector();
            
            // Tell the hosted game about our connector so that it handles our requests and
            // broadcasts information to us.
            hostedGame.AddCorporationConnector(inMemoryConnector);

            // -------- THIS PART HAPPENS ON EACH PLAYER'S COMPUTER -------- 

            // In an internet game, we would create an instance of a client-side connector here.
            // However, as this game is in-memory, we can use the same connector which implements
            // both the server-side and the client-side interfaces.
            // Creating the connector would require us to provide the hosted games's
            // URL/IP address, some kind of ID that identifies the game we want to join
            // and some credentials that identify which player we are.

            // Lastly, create the local game. This game only has partial information and
            // provides no way for the Corporation to view the Runner's cards by hacking the code.
            CorporationGame corporationGame = new CorporationGame(inMemoryConnector);

            // Most games have a "game loop" or "pump" which repeatedly alternates calls to
            // "Draw()" and "Update()". Stuff only happens during the call to Update to avoid
            // changing anything whilst it is being drawn. XNA handles this for us, but for now
            // we can simulate it by calling "Update" on our connector.
            inMemoryConnector.Update();

            // If you execute this code, you'll see that the CorporationGame makes a request
            // to the hosted game for a copy of the game state, which it then loads.

            // OK - So here we go with some sample actions:
            corporationGame.TakeAction(new CorporationPasses());
            inMemoryConnector.Update();

            // This action should originate from the runner's game, but it's a hack for now.
            corporationGame.TakeAction(new RunnerPasses());
            inMemoryConnector.Update();

            // The hosted game should now respond by sending the corporation game 2 actions:
            // - Make Card Visible
            // - Corporation Draws Card

            // This concludes the draw phase so we should now be in the action phase.

            System.Console.WriteLine(corporationGame.StateMachine.State);

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
