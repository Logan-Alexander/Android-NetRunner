using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpNeutral
{
    [CardBehaviourID(CardSet.CoreSet, 113, "Wall of Static")]
    public class WallOfStatic : PieceOfIceCardBehaviour
    {
        public WallOfStatic(Card card)
            : base(
                card,
                0,
                CorporationFaction.None,
                3,
                3,
                IceTypes.Barrier)
        {
            Subroutine subroutine1 = new Subroutine();
            subroutine1.Effects.Add(new EndTheRun());
            SubRoutines.Add(subroutine1);
        }
    }
}
