using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    public class Ice : CorporationCard
    {
        public int RezCost { get; private set; }
        public int Strength { get; private set; }
        public IceTypes IceTypes { get; private set; }
        public List<Subroutine> SubRoutines;

        public Ice(int id, string title, int influence, CorporationFaction faction, int rezCost, int strength, IceTypes iceTypes)
            : base(id, title, influence, faction)
        {
            RezCost = rezCost;
            Strength = strength;
            IceTypes = iceTypes;
            SubRoutines = new List<Subroutine>();
        }
    }
}
