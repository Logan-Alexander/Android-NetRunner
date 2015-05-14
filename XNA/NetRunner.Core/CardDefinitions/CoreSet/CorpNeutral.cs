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
    public partial class CardFactory
    {
        protected Operation CreateHedgeFund()
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
        protected PieceOfIce CreateEnigma()
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
        protected PieceOfIce CreateWallOfStatic()
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
    }
}
