using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetRunner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    public interface ICardsUI
    {
        bool TryFindCard(Card card, out DrawableCard drawableCard);

        DrawableCard CreateCard(Card card, ICardLocation location);
    }

    /// <summary>
    /// This component will draw a set of cards. These cards are purely for the UI and
    /// have a loose relationship with the cards in the actual game. This enables us
    /// to do animations and other stuff to the cards rather than everything happening
    /// instanteously.
    /// </summary>
    public class CardsUI : DrawableGameComponent, ICardsUI
    {
        private ICamera _Camera;

        private VertexBuffer _VertexBuffer;
        private IndexBuffer _IndexBuffer;
        private BasicEffect _Effect;

        private Texture2D _CorporationTexture;
        private Texture2D _RunnerTexture;
        private Texture2D _HedgeFundTexture;

        private List<DrawableCard> _Cards;

        public CardsUI(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(ICardsUI), this);
            
            _Cards = new List<DrawableCard>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _Camera = (ICamera)Game.Services.GetService(typeof(ICamera));
        }

        protected override void LoadContent()
        {
            _Effect = new BasicEffect(GraphicsDevice);
            _Effect.TextureEnabled = true;
            _Effect.LightingEnabled = false;
            _Effect.VertexColorEnabled = false;

            // Our model of the card in 3D space will be centered around the origin (0, 0, 0).
            // The top of the card will be pointing in the +Y axis.
            // The front of the card will be facing the -Z axis. This is a little confusing
            // as when we draw the card we will likely want to see the face. It's just a
            // convention that models face "forwards" when they are looking to the -Z axis.

            // Top-left of card (when looking at the front)
            VertexPositionTexture v1 = new VertexPositionTexture(
                new Vector3(0.1f, 0.14f, 0),
                new Vector2(0, 0));

            // Top-right of card (when looking at the front)
            VertexPositionTexture v2 = new VertexPositionTexture(
                new Vector3(-0.1f, 0.14f, 0),
                new Vector2(1, 0));

            // Bottom-left of card (when looking at the front)
            VertexPositionTexture v3 = new VertexPositionTexture(
                new Vector3(0.1f, -0.14f, 0),
                new Vector2(0, 1));

            // Bottom-right of card (when looking at the front)
            VertexPositionTexture v4 = new VertexPositionTexture(
                new Vector3(-0.1f, -0.14f, 0),
                new Vector2(1, 1));

            VertexPositionTexture[] vertices = new VertexPositionTexture[4]
            {
                v1, v2, v3, v4
            };

            _VertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), 6, BufferUsage.WriteOnly);
            _VertexBuffer.SetData(vertices);
            
            short[] indicies = new short[12]
            {
                // Primitives defined in clockwise order
                0, 1, 2, 2, 1, 3, //Front
                3, 1, 2, 2, 1, 0 //Back
            };

            _IndexBuffer = new IndexBuffer(GraphicsDevice, typeof(short), 12, BufferUsage.WriteOnly);
            _IndexBuffer.SetData(indicies);

            // Load textures
            _CorporationTexture = Game.Content.Load<Texture2D>("Textures/CardBackCorporation");
            //_RunnerTexture = Game.Content.Load<Texture2D>("Textures/CardBackRunner");
            _HedgeFundTexture = Game.Content.Load<Texture2D>("Textures/CardFrontHedgeFund");
        }

        protected override void UnloadContent()
        {
            _Effect.Dispose();
            _Effect = null;

            _VertexBuffer.Dispose();
            _VertexBuffer = null;

            _IndexBuffer.Dispose();
            _IndexBuffer = null;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (DrawableCard card in _Cards)
            {
                card.Animation.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetVertexBuffer(_VertexBuffer);
            GraphicsDevice.Indices = _IndexBuffer;

            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            _Effect.View = _Camera.ViewMatrix;
            _Effect.Projection = _Camera.PerspectiveMatrix;

            foreach (DrawableCard drawableCard in _Cards)
            {
                _Effect.World = Matrix.CreateWorld(
                    drawableCard.Location.Position,
                    drawableCard.Location.Forward,
                    drawableCard.Location.Up);

                if (drawableCard.Card.CardIsIdentified)
                {
                    _Effect.Texture = GetFrontTexture(drawableCard.Card.Behaviour);
                    foreach (EffectPass pass in _Effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                    }
                }

                _Effect.Texture = _Effect.Texture = GetBackTexture(drawableCard.Card.PlayerType);
                foreach (EffectPass pass in _Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 6, 2);
                }
            }
        }

        private Texture2D GetBackTexture(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Corporation:
                    return _CorporationTexture;

                case PlayerType.Runner:
                    return _RunnerTexture;

                default:
                    throw new NotSupportedException();
            }
        }

        private Texture2D GetFrontTexture(CardBehaviour cardBehaviour)
        {
            switch (cardBehaviour.Title)
            {
                case "Hedge Fund":
                    return _HedgeFundTexture;

                default:
                    throw new NotSupportedException();
            }
        }

        public bool TryFindCard(Card card, out DrawableCard drawableCard)
        {
            drawableCard = _Cards.SingleOrDefault(c => c.Card == card);

            return drawableCard != null;
        }

        public DrawableCard CreateCard(Card card, ICardLocation location)
        {
            if (_Cards.Any(c => c.Card == card))
            {
                throw new InvalidOperationException("A DrawableCard already exists for this card.");
            }

            DrawableCard drawableCard = new DrawableCard(card, location);
            
            _Cards.Add(drawableCard);
            
            return drawableCard;
        }
    }
}
