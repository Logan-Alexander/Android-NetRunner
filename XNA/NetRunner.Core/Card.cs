using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
