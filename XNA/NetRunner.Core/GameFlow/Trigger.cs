using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    /// <summary>
    /// A list of the triggers that can be used to indicate an action.
    /// Every action that modifies the flow will fire one of these triggers.
    /// </summary>
    public enum Trigger
    {
        // Common Triggers,
        Auto,
        ChildStateMachineComplete,

        // Main triggers
        GameStarts,

        // Turn Order triggers
        CorporationDrawsCardAtStartOfTurn,

        // Paid Ability Window triggers
        CorporationUsesPaidAbility,
        CorporationRezzesNonIce,
        CorporationScoresAgenda,
        CorporationPasses,
        RunnerUsesPaidAbility,
        RunnerPasses,

        // Corporation Action triggers
        CorporationDrawsCardAsAction,
        CorporationTakesOneCredit,

        // Corporation disacrd phase triggers
        CorporationDiscardsCardFromHQ,
    }
}
