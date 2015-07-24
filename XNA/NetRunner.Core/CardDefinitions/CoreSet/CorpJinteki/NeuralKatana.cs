using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki
{
    [CardBehaviourID(CardSet.CoreSet, 77, "Neural Katana")]
    public class NeuralKatana : PieceOfIceCardBehaviour
    {
        public NeuralKatana(Card card)
            : base(
                card,
                2,
                CorporationFaction.Jinteki,
                4,
                3,
                IceTypes.Sentry)
        {
            Subroutine subroutine1 = new Subroutine();
            subroutine1.Effects.Add(new DamageTheRunner(DamageType.Net, 3));
            SubRoutines.Add(subroutine1);
        }
    }
}
