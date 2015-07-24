using NetRunner.Core.Conditions;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using NetRunner.Core.Selectors;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki
{
    [CardBehaviourID(CardSet.CoreSet, 75, "Chum")]
    public class Chum : PieceOfIceCardBehaviour
    {
        public Chum(Card card)
            : base(
                card,
                1,
                CorporationFaction.Jinteki,
                1,
                4,
                IceTypes.CodeGate)
        {
            Subroutine subroutine1 = new Subroutine();
            // TODO: Limit this to the next ENCOUNTER rather than the next piece of ice.

            ContinuousEffect effect1 = new ModifyIceStrength(
                new NextPieceOfIce(),
                2);
            effect1.Conditions.Add(new CurrentRun());
            subroutine1.Effects.Add(effect1);

            PostTrigger trigger1 = new IceEncounterEnds(new NextPieceOfIce());
            InstantEffect effect2 = new DamageTheRunner(DamageType.Net, 3);
            effect2.Conditions.Add(new Not(new RunnerBreaksAllSubroutinesOnIce(new NextPieceOfIce())));
            trigger1.Effects.Add(effect2);
            subroutine1.Triggers.Add(trigger1);

            SubRoutines.Add(subroutine1);
        }
    }
}
