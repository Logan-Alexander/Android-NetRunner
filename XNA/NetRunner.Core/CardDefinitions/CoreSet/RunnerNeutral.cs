using NetRunner.Core.Conditions;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Selectors;
using NetRunner.Core.InstantEffects;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetRunner.Core.Corporation;
using NetRunner.Core.Runner;

namespace NetRunner.Core
{
    public partial class CardFactory
    {
        protected EventCard CreateSureGamble()
        {
            EventCard card = new EventCard(
                    0,
                    "Sure Gamble",
                    0,
                    RunnerFaction.None,
                    5);
                card.AddEffect(new AddCredits(PlayerType.Runner, 9));
            return card;
        }
    }
}
