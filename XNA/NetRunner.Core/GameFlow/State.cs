using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public enum State
    {
        Setup,
        CorporationTurn,
        CorporationDrawPhase,
        CorporationDrawPhasePaidAbilityWindow,
        CorporationDrawPhasePaidAbilityWindowCorporation,
        CorporationDrawPhasePaidAbilityWindowRunner,
        CorporationDrawPhaseDrawingCard,
        CorporationActionPhase,
    }
}
