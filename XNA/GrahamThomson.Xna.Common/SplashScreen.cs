using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GrahamThomson.Xna.Common
{
    public class SplashScreen : DrawableGameComponent
    {
        public event EventHandler<EventArgs> Completed;
        private void OnCompleted(EventArgs e)
        {
            EventHandler<EventArgs> temp = Completed;
            if (temp != null)
            {
                temp(this, e);
            }
        }
        
        private float _Rotation;
        private float _Age;

        private ContentManager _ContentManager;
        private BasicEffect _Effect;
        private Texture2D _TextureLogo;
        private Texture2D _TextureRays;

        private SoundEffect _SoundEffectBoom;
        private SoundEffect _SoundEffectWail;
        private bool _SoundStartedBoom;
        private bool _SoundStartedWail;

        private float _MasterAlpha = 1f;
        private bool _Completed;

        public SplashScreen(Game game)
            : base(game)
        {
            Game.Components.Add(this);
            Game.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }

        public override void Initialize()
        {
            base.Initialize();

            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;
            samplerState.AddressW = TextureAddressMode.Clamp;
            GraphicsDevice.SamplerStates[0] = samplerState;

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        protected override void LoadContent()
        {
            _ContentManager = new ContentManager(Game.Services, "Common");
            _TextureLogo = _ContentManager.Load<Texture2D>("Textures/Logo");
            _TextureRays = _ContentManager.Load<Texture2D>("Textures/LogoRays");
            _SoundEffectBoom = _ContentManager.Load<SoundEffect>("SoundEffects/LogoBoom");
            _SoundEffectWail = _ContentManager.Load<SoundEffect>("SoundEffects/LogoWail");

            _Effect = new BasicEffect(GraphicsDevice);
            _Effect.World = Matrix.CreateWorld(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            _Effect.View = Matrix.CreateLookAt(-Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            _Effect.Projection = Matrix.CreateOrthographic(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0.1f, 10f);
            _Effect.LightingEnabled = false;
            _Effect.TextureEnabled = true;
            _Effect.VertexColorEnabled = true;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (_Effect != null)
            {
                _Effect.Projection = Matrix.CreateOrthographic(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0.1f, 10f);
            }
        }

        protected override void UnloadContent()
        {
            _ContentManager.Unload();
            _ContentManager.Dispose();
            _Effect.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.075f;
            _Age += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_Age > 0 && !_SoundStartedWail)
            {
                _SoundStartedWail = true;
                _SoundEffectWail.Play(0.35f, 0f, 0f);
            }

            if (_Age > 3 && !_SoundStartedBoom)
            {
                _SoundStartedBoom = true;
                _SoundEffectBoom.Play();
            }

            if (_Age > 8)
            {
                _MasterAlpha = 1 - MathHelper.Clamp((_Age - 8) * 0.25f, 0, 1);
            }

            if (_Age >= 12 && !_Completed)
            {
                _Completed = true;
                OnCompleted(new EventArgs());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            float width = CalculateWidth();
            DrawRays(width);
            DrawLogo(width);
        }

        private float CalculateWidth()
        {
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
            float width = 1536f;
            if (titleSafeArea.Width < width)
            {
                width = titleSafeArea.Width;
            }
            width *= 0.8f;
            float height = width * (512f / 1536f);
            return width;
        }

        private IEnumerable<VertexPositionColorTexture> CalculateLogoVerts()
        {
            float alpha1 = MathHelper.Clamp((_Age - 1) * 0.25f, 0, 1) * _MasterAlpha;
            float alpha2 = MathHelper.Clamp((_Age - 3) * 0.25f, 0, 1) * _MasterAlpha;

            VertexPositionColorTexture topLeft = new VertexPositionColorTexture(new Vector3(-768f, 256f, 0), Color.White * alpha1, new Vector2(0, 0));
            VertexPositionColorTexture topRight = new VertexPositionColorTexture(new Vector3(768f, 256f, 0), Color.White * alpha2, new Vector2(1, 0));
            VertexPositionColorTexture bottomLeft = new VertexPositionColorTexture(new Vector3(-768f, -256f, 0), Color.White * alpha1, new Vector2(0, 1));
            VertexPositionColorTexture bottomRight = new VertexPositionColorTexture(new Vector3(768f, -256f, 0), Color.White * alpha2, new Vector2(1, 1));

            yield return topLeft;
            yield return topRight;
            yield return bottomLeft;
            yield return topRight;
            yield return bottomRight;
            yield return bottomLeft;
        }

        private IEnumerable<VertexPositionColorTexture> CalculateRayVerts()
        {
            Color color = Color.White * MathHelper.Clamp((_Age - 3) * 0.25f, 0, 1) * _MasterAlpha;

            VertexPositionColorTexture topLeft = new VertexPositionColorTexture(new Vector3(-256f, 256f, 0), color, new Vector2(0, 0));
            VertexPositionColorTexture topRight = new VertexPositionColorTexture(new Vector3(256f, 256f, 0), color, new Vector2(1, 0));
            VertexPositionColorTexture bottomLeft = new VertexPositionColorTexture(new Vector3(-256f, -256f, 0), color, new Vector2(0, 1));
            VertexPositionColorTexture bottomRight = new VertexPositionColorTexture(new Vector3(256f, -256f, 0), color, new Vector2(1, 1));

            yield return topLeft;
            yield return topRight;
            yield return bottomLeft;
            yield return topRight;
            yield return bottomRight;
            yield return bottomLeft;
        }

        private void DrawLogo(float width)
        {
            _Effect.World = Matrix.CreateWorld(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            _Effect.World *= Matrix.CreateScale(width / 1536f);
            _Effect.Texture = _TextureLogo;

            IEnumerable<VertexPositionColorTexture> verts = CalculateLogoVerts();

            foreach (EffectPass pass in _Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, verts.ToArray(), 0, 2);
            }
        }

        private void DrawRays(float width)
        {
            _Effect.World = Matrix.CreateWorld(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            _Effect.World *= Matrix.CreateRotationZ(_Rotation);
            _Effect.World *= Matrix.CreateTranslation(-new Vector3(-516f, 0, 0));
            _Effect.World *= Matrix.CreateScale(width / 1536f);
            _Effect.Texture = _TextureRays;

            IEnumerable<VertexPositionColorTexture> verts = CalculateRayVerts();

            foreach (EffectPass pass in _Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, verts.ToArray(), 0, 2);
            }
        }
    }
}
