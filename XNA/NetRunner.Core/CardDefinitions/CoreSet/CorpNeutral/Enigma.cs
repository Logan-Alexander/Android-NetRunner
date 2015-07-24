using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpNeutral
{
    [CardBehaviourID(CardSet.CoreSet, 111, "Enigma")]
    public class Enigma : PieceOfIceCardBehaviour
    {
        public Enigma(Card card)
            : base(
                card,
                0,
                CorporationFaction.None,
                3,
                2,
                IceTypes.CodeGate)
        {
            Subroutine subroutine1 = new Subroutine();
            subroutine1.Effects.Add(new LoseClicks(1));
            SubRoutines.Add(subroutine1);

            Subroutine subroutine2 = new Subroutine();
            subroutine2.Effects.Add(new EndTheRun());
            SubRoutines.Add(subroutine2);
        }
    }
}
