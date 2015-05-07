using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class PieceOfIce : CorporationCard, IRezableCard, IServerCard
    {
        public int RezCost { get; private set; }
        public int Strength { get; private set; }
        public IceTypes IceTypes { get; private set; }
        public List<Subroutine> SubRoutines;

        public Server Server { get; set; }

        public PieceOfIce(int id, string title, int influence, CorporationFaction faction, int rezCost, int strength, IceTypes iceTypes)
            : base(id, title, influence, faction)
        {
            RezCost = rezCost;
            Strength = strength;
            IceTypes = iceTypes;
            SubRoutines = new List<Subroutine>();
        }
    }
}
