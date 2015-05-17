using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    [Flags]
    public enum PaidAbilityWindowOptions
    {
        UsePaidAbilities = 1,
        RezNonIce = 2,
        ScoreAgendas = 4,
        UseRezScore = UsePaidAbilities | RezNonIce | ScoreAgendas,
    }
}
