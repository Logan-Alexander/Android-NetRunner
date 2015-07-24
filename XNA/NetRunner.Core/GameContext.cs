using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Corporation;
using NetRunner.Core.Runner;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    /// <summary>
    /// The game context provides information about the physical objects on the table
    /// in a game of Net Runner.
    /// </summary>
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
        public int CorporationCredits { get; set; }

        //TODO: Move this to the flow. The "current run" is a game flow concept and not a physical thing.
        public Run CurrentRun { get; set; }

        public Card RunnerFactionCard { get; private set; }
        public RunnerStack Stack { get; private set; }
        public Heap Heap { get; private set; }
        public ResourceRow ResourceRow { get; private set; }
        public HardwareRow HardwareRow { get; private set; }
        public ProgramRow ProgramRow { get; private set; }
        public Grip Grip { get; private set; }
        public RunnerScoreArea RunnerScoreArea { get; private set; }

        public HeadQuarters HeadQuarters { get; private set; }
        public ResearchAndDevelopment ResearchAndDevelopment { get; private set; }
        public Archives Archives { get; private set; }
        public List<RemoteServer> RemoteServers { get; private set; }
        public CorporationScoreArea CorporationScoreArea { get; private set; }

        /*
         * TODO: Think about how effects are handled.
         * If a paid ability has been used and has an effect which lasts for the turn
         * (E.g. the runner uses "Net Shield" to prevent 1 net damage)
         * How should this be represented?
         * 
         * It's not a state change so it's not part of the game flow.
         * It's not a physical object so it's not part of the game context.
         * 
         * You could argue that activated effects could be marked with a physical token
         * on the board, in which case the context is the right place for them.
         * 
         * You could argue also that the card behaviours themselves should track this
         * information.
         * 
         * Or you could argue that this is a third kind of information which needs it's own
         * representation.
         * */
        public List<ContinuousEffect> ActiveContinuousEffects { get; private set; }

        public GameContext()
        {
            Stack = new RunnerStack();
            Heap = new Heap();
            ResourceRow = new ResourceRow();
            HardwareRow = new HardwareRow();
            ProgramRow = new ProgramRow();
            Grip = new Grip();
            RunnerScoreArea = new RunnerScoreArea();

            HeadQuarters = new HeadQuarters();
            ResearchAndDevelopment = new ResearchAndDevelopment();
            Archives = new Archives();
            RemoteServers = new List<RemoteServer>();
            CorporationScoreArea = new Corporation.CorporationScoreArea();

            ActiveContinuousEffects = new List<ContinuousEffect>();
        }

        public GameContext(GameSetup gameSetup)
            : this()
        {
            SetRunnerFaction(gameSetup.RunnerFaction);
            SetCorporationFaction(gameSetup.CorporationFaction);

            foreach (CardBehaviourID cardBehaviourID in gameSetup.RunnerDeck)
            {
                Card card = new Card(PlayerType.Runner);
                card.IdentifyCard(cardBehaviourID);
                Stack.AddCard(card);
            }

            Stack.Shuffle();

            for (int index = 0; index < 5; ++index)
            {
                Grip.Add(Stack.RemoveTopCard());
            }

            foreach (CardBehaviourID cardBehaviourID in gameSetup.CorporationDeck)
            {
                Card card = new Card(PlayerType.Corporation);
                card.IdentifyCard(cardBehaviourID);
                ResearchAndDevelopment.AddCard(card);
            }

            ResearchAndDevelopment.Shuffle();

            for (int index = 0; index < 5; ++index)
            {
                HeadQuarters.Hand.Add(ResearchAndDevelopment.RemoveTopCard());
            }

            RunnerCredits = 5;
            CorporationCredits = 5;
        }

        private void SetRunnerFaction(RunnerFaction faction)
        {
            Card card = new Card(PlayerType.Runner);

            switch (faction)
            {
                case RunnerFaction.Anarch:
                    card.IdentifyCard("Noise");
                    break;

                //TODO: Add other faction cards.

                default:
                    throw new NotSupportedException();
            }

            RunnerFactionCard = card;
        }

        private void SetCorporationFaction(CorporationFaction faction)
        {
            Card card = new Card(PlayerType.Corporation);

            switch (faction)
            {
                case CorporationFaction.Jinteki:
                    card.IdentifyCard("Jinteki");
                    break;

                //TODO: Add other faction cards.

                default:
                    throw new NotSupportedException();
            }

            HeadQuarters.FactionCard = card;
        }
    }
}
