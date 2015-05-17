using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public enum Trigger
    {
        Auto,
        GameStarts,
        CorporationUsesPaidAbility,
        CorporationRezzesNonIce,
        CorporationScoresAgenda,
        CorporationPasses,
        RunnerUsesPaidAbility,
        RunnerPasses,
        CorporationCardDrawn
    }
}
