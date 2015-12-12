using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetRunner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna
{
    /// <summary>
    /// Represents a card that will be drawn. This is loosely coupled to a card in the
    /// actual game, but those cards move around instantly and we will want this card's
    /// position to animate.
    /// 
    /// To draw a card, all we really need to know is the texture on the front and back
    /// and the card's location.
    /// 
    /// We may need to consider the DrawOrder if we treat the cards like sprites as cards
    /// laying on top of one another may be too close together for the depth buffer to
    /// handle properly.
    /// </summary>
    public class DrawableCard
    {
        public Card Card { get; set; }

        public Texture2D FrontTexture { get; set; }
        public Texture2D BackTexture { get; set; }

        public ICardAnimation Animation { get; set; }
        
        public ICardLocation Location
        {
            get { return Animation.Location; }
        }

        public DrawableCard(Card card, ICardLocation location)
        {
            Card = card;
            Animation = new NoAnimation(location);
        }
    }

    public interface ICardAnimation
    {
        ICardLocation Location { get; }
        void Update(GameTime gameTime);
    }

    public class NoAnimation: ICardAnimation
    {
        public ICardLocation Location { get; private set; }

        public NoAnimation(ICardLocation location)
        {
            Location = location;
        }

        public void Update(GameTime gameTime)
        {
        }
    }

    /// <summary>
    /// Simply fixes a card in 3D space. No animation, no fuss.
    /// </summary>
    public class StationaryCardLocation : ICardLocation
    {
        public Vector3 Position { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Up { get; set; }
    }

    /// <summary>
    /// Defines the position of a card in 3D space.
    /// </summary>
    public interface ICardLocation
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Vector3 Up { get; }
    }
}
