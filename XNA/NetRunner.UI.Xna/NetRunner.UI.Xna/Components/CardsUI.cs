using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    /// <summary>
    /// This component will draw a set of cards. These cards are purely for the UI and
    /// have a loose relationship with the cards in the actual game. This enables us
    /// to do animations and other stuff to the cards rather than everything happening
    /// instanteously.
    /// </summary>
    public class CardsUI : DrawableGameComponent
    {
        private ICamera _Camera;

        private VertexBuffer _VertexBuffer;
        private IndexBuffer _IndexBuffer;
        private BasicEffect _Effect;

        public List<Card> Cards {get; set;}

        public CardsUI(Game game)
            : base(game)
        {
            Cards = new List<Card>();
            
            game.Components.Add(this);
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
            foreach (Card card in Cards)
            {
                card.Location.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetVertexBuffer(_VertexBuffer);
            GraphicsDevice.Indices = _IndexBuffer;

            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            _Effect.View = _Camera.ViewMatrix;
            _Effect.Projection = _Camera.PerspectiveMatrix;

            foreach (Card card in Cards)
            {
                _Effect.World = Matrix.CreateWorld(
                    card.Location.Position,
                    card.Location.Forward,
                    card.Location.Up);

                _Effect.Texture = card.FrontTexture;
                foreach (EffectPass pass in _Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                }

                _Effect.Texture = card.BackTexture;
                foreach (EffectPass pass in _Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 6, 2);
                }
            }
        }
    }
}
