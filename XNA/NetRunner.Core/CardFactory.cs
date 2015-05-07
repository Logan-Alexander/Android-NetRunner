using NetRunner.Core.Conditions;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Selectors;
using NetRunner.Core.InstantEffects;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetRunner.Core.Corporation;
using NetRunner.Core.Runner;

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

                case "Neural EMP":
                    {
                        Operation card = new Operation(
                            72,
                            "Neural EMP",
                            2,
                            CorporationFaction.Jinteki,
                            2);

                        card.PlayConditions.Add(new RunLastTurn());
                        card.Effects.Add(new DamageTheRunner(DamageType.Net, 1));

                        return card;
                    }

                case "Precognition":
                    {
                        Operation card = new Operation(
                            73,
                            "Precognition",
                            3,
                            CorporationFaction.Jinteki,
                            0);

                        card.Effects.Add(new ArrangeTopFiveCardsOfResearchAndDevelopment());
                        
                        return card;
                    }

                case "Chum":
                    {
                        PieceOfIce card = new PieceOfIce(
                            75,
                            "Chum",
                            1,
                            CorporationFaction.Jinteki,
                            1,
                            4,
                            IceTypes.CodeGate);
                        
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

                        card.SubRoutines.Add(subroutine1);

                        return card;
                    }

                case "Neural Katana":
                    {
                        PieceOfIce card = new PieceOfIce(
                            77,
                            "Neural Katana",
                            2,
                            CorporationFaction.Jinteki,
                            4,
                            3,
                            IceTypes.Sentry);

                        Subroutine subroutine1 = new Subroutine();
                        subroutine1.Effects.Add(new DamageTheRunner(DamageType.Net, 3));
                        card.SubRoutines.Add(subroutine1);

                        return card;
                    }


                case "Wall of Thorns":
                    {
                        PieceOfIce card = new PieceOfIce(
                            78,
                            "Wall of Thorns",
                            1,
                            CorporationFaction.Jinteki,
                            8,
                            5,
                            IceTypes.Barrier);

                        Subroutine subroutine1 = new Subroutine();
                        subroutine1.Effects.Add(new DamageTheRunner(DamageType.Net, 2));
                        card.SubRoutines.Add(subroutine1);

                        Subroutine subroutine2 = new Subroutine();
                        subroutine2.Effects.Add(new EndTheRun());
                        card.SubRoutines.Add(subroutine2);

                        return card;
                    }

                case "Akitaro Watanabe":
                    {
                        Upgrade card = new Upgrade(
                            79,
                            "Akitaro Watanabe",
                            2,
                            CorporationFaction.Jinteki,
                            1);

                        ContinuousEffect effect1 = new ModifyRezCost(
                            new IceProtectingServer(new ThisCardsServer(card)),
                            -2);

                        card.Effects.Add(effect1);

                        return card;
                    }

                case "Hedge Fund":
                    {
                        Operation card = new Operation(
                            110,
                            "Hedge Fund",
                            0,
                            CorporationFaction.None,
                            5);

                        card.Effects.Add(new AddCredits(PlayerType.Corporation, 9));

                        return card;
                    }

                case "Enigma":
                    {
                        PieceOfIce card = new PieceOfIce(
                            111,
                            "Enigma",
                            0,
                            CorporationFaction.None,
                            3,
                            2,
                            IceTypes.CodeGate);

                        Subroutine subroutine1 = new Subroutine();
                        subroutine1.Effects.Add(new LoseClicks(1));
                        card.SubRoutines.Add(subroutine1);

                        Subroutine subroutine2 = new Subroutine();
                        subroutine2.Effects.Add(new EndTheRun());
                        card.SubRoutines.Add(subroutine2);

                        return card;
                    }

                case "Wall of Static":
                    {
                        PieceOfIce card = new PieceOfIce(
                            113,
                            "Wall of Static",
                            0,
                            CorporationFaction.None,
                            3,
                            3,
                            IceTypes.Barrier);

                        Subroutine subroutine1 = new Subroutine();
                        subroutine1.Effects.Add(new EndTheRun());
                        card.SubRoutines.Add(subroutine1);

                        return card;
                    }


                default:
                    throw new NotSupportedException();
            }
        }
    }
}
