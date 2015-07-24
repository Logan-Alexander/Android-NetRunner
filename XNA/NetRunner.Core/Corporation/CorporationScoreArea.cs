using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class CorporationScoreArea
    {
        public List<Card> Agendas { get; private set; }

        public CorporationScoreArea()
        {
            Agendas = new List<Card>();
        }
    }
}
