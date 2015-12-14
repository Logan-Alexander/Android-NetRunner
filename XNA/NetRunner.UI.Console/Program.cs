using NetRunner.Core;
using NetRunner.Core.Actions;
using NetRunner.Core.CardIdentifiers;
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
            GameSetup setup = new GameSetup();
            setup.CorporationFaction = Core.Corporation.CorporationFaction.Jinteki;
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));

            // -------- IN AN INTERNET GAME, THIS PART WOULD HAPPEN ON THE WEB SERVER -------- 

            // Create the GameContext. This will hold all information about the game.
            GameContext gameContext = new GameContext(setup);
            Flow stack = new Flow(gameContext);
            stack.Fire(Trigger.GameStarts);

            // Create a HostedGame.
            // This will allow information about the game to be broadcast to all players.
            // The information sent to the Runner and the Corporation will be different.
            // For example, when the Corporation draws a card, the Corporation will be told
            // which card was moved from R&D to Headquarters. The Runner will only be told
            // that a card was moved.
            HostedGame hostedGame = new HostedGame(gameContext, stack);

            // An "in-memory" game connector allows information to flow directly from clients
            // to the hosted game. In a local game, this will be sufficient.
            // For an internet game, the web server would create an instance
            // of some connector that implemented the server-side interface.
            InMemoryGameConnector inMemoryConnector = new InMemoryGameConnector();
            
            // Tell the hosted game about our connector so that it handles our requests and
            // broadcasts information to us.
            hostedGame.AddCorporationConnector(inMemoryConnector);
            hostedGame.AddRunnerConnector(inMemoryConnector);

            // -------- THIS PART HAPPENS ON THE RUNNER PLAYER'S COMPUTER -------- 

            RunnerGame runnerGame = new RunnerGame(inMemoryConnector);

            // -------- THIS PART HAPPENS ON THE CORPORATION PLAYER'S COMPUTER -------- 

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
            AssetOrAgendaIdentifier cardIdentifier = new AssetOrAgendaIdentifier(0);
            corporationGame.TakeAction(new CorporationScoresAgenda(cardIdentifier));
            corporationGame.TakeAction(new CorporationPasses());
            inMemoryConnector.Update();

            runnerGame.TakeAction(new RunnerPasses());
            inMemoryConnector.Update();

            // We need to call update here twice as runner actions are processed after corp actions.
            // When used in a real game loop, this wont be an issue (aside from a 1 frame delay).
            inMemoryConnector.Update();

            corporationGame.TakeAction(new CorporationDrawsCard());
            inMemoryConnector.Update();

            // This concludes the draw phase so we should now be in the action phase.

            System.Console.WriteLine(corporationGame.Flow);

            System.Console.ReadKey();
        }

        static void DoStuff()
        {
            GameSetup setup = new GameSetup();
            setup.CorporationFaction = Core.Corporation.CorporationFaction.Jinteki;
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));
            setup.CorporationDeck.Add(new CardBehaviourID(CardSet.CoreSet, 110));

            GameContext context = new GameContext(setup);
            context.CurrentRun = new Run();

            // Set up a piece of "Chum" Ice.
            Card card1 = new Card(PlayerType.Corporation);
            card1.IdentifyCard("Chum");
            PieceOfIceCardBehaviour ice1 = (PieceOfIceCardBehaviour)card1.Behaviour;

            context.OnIceEncounterStarted(new IceEventArgs(context, ice1));

            foreach (Subroutine subroutine in ice1.SubRoutines)
            {
                if (!subroutine.IsBroken)
                {
                    subroutine.Execute(context);
                }
            }

            context.OnIceEncounterEnded(new IceEventArgs(context, ice1));

            // Set up another piece of ice (in the same run) to be affected by the Chum.
            Card card2 = new Card(PlayerType.Corporation);
            card2.IdentifyCard("Wall of Thorns");
            PieceOfIceCardBehaviour ice2 = (PieceOfIceCardBehaviour)card2.Behaviour;
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
