using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public enum Trigger
    {
        // Common Triggers,
        Auto,
        ChildStateMachineComplete,

        // Main triggers
        GameStarts,

        // Turn Order triggers
        CorporationCardDrawn,

        // Paid Ability Window triggers
        CorporationUsesPaidAbility,
        CorporationRezzesNonIce,
        CorporationScoresAgenda,
        CorporationPasses,
        RunnerUsesPaidAbility,
        RunnerPasses,
    }
}
