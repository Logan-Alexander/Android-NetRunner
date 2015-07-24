using NetRunner.Core.Corporation;
using NetRunner.Core.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    /// <summary>
    /// Defines the decks that the corporation and runner bring to the table.
    /// These properties should probably be moved into separate classse called
    /// CorporationDeck and RunnerDeck which can provide some validation
    /// (deck size, out of faction influence, agenda points, etc).
    /// </summary>
    public class GameSetup
    {
        public CorporationFaction CorporationFaction { get; set; }
        public List<CardBehaviourID> CorporationDeck { get; set; }

        public RunnerFaction RunnerFaction { get; set; }
        public List<CardBehaviourID> RunnerDeck { get; set; }

        public GameSetup()
        {
            CorporationDeck = new List<CardBehaviourID>();
            RunnerDeck = new List<CardBehaviourID>();
        }
    }
}
