using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public class RunnerScoreArea
    {
        public List<Card> Agendas { get; private set; }

        public RunnerScoreArea()
        {
            Agendas = new List<Card>();
        }
    }
}
