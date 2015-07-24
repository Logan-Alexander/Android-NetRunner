using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class HeadQuarters : Server
    {
        public Card FactionCard { get; set; }
        public List<Card> Hand { get; private set; }

        public HeadQuarters()
            : base(ServerType.Central)
        {
            Hand = new List<Card>();
        }
    }
}
