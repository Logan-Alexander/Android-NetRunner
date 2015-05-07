using System;
using System.Collections.Generic;
using System.Linq;

namespace NetRunner.Core
{
    public abstract class Card
    {
        public int ID { get; private set; }
        public string Title { get; private set; }
        public PlayerType PlayerType { get; private set; }
        public int Influence { get; private set; }

        public Card(int id, string title, PlayerType playerType, int influence)
        {
            ID = id;
            Title = title;
            PlayerType = playerType;
            Influence = influence;
        }

        public override string ToString()
        {
            return string.Format("({0}: {1})", ID, Title);
        }
    }
}
