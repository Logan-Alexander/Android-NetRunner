using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class PieceOfIceCardBehaviour : CorporationCardBehaviour, IRezableCard, IServerCard
    {
        public int RezCost { get; private set; }
        public int Strength { get; private set; }
        public IceTypes IceTypes { get; private set; }
        public List<Subroutine> SubRoutines;

        public Server Server { get; set; }

        public PieceOfIceCardBehaviour(Card card, int influence, CorporationFaction faction, int rezCost, int strength, IceTypes iceTypes)
            : base(card, influence, faction)
        {
            RezCost = rezCost;
            Strength = strength;
            IceTypes = iceTypes;
            SubRoutines = new List<Subroutine>();
        }
    }
}
