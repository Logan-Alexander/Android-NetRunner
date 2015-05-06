using NetRunner.Core.Conditions;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.IdentifierPredicates;
using NetRunner.Core.InstantEffects;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    public class CardFactory
    {
        public Card Create(string name)
        {
            switch (name)
            {
                case "Sure Gamble":
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

                case "Chum":
                    {
                        Ice card = new Ice(
                            75,
                            "Chum",
                            1,
                            CorporationFaction.Jinteki,
                            1,
                            4,
                            IceTypes.CodeGate);
                        
                        Subroutine subroutine1 = new Subroutine();
                        
                        ContinuousEffect effect1 = new ModifyIceStrength(new NextPieceOfIce(), 2);
                        effect1.Conditions.Add(new CurrentRun());
                        subroutine1.Effects.Add(effect1);

                        PostTrigger trigger1 = new IceEncounterEnds(new NextPieceOfIce());
                        InstantEffect effect2 = new DamageTheRunner(DamageType.Net, 3);
                        effect2.Conditions.Add(new Not(new RunnerBreaksAllSubroutinesOnIce(new NextPieceOfIce())));
                        trigger1.Effects.Add(effect2);
                        subroutine1.Triggers.Add(trigger1);

                        card.SubRoutines.Add(subroutine1);

                        return card;
                    }

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
