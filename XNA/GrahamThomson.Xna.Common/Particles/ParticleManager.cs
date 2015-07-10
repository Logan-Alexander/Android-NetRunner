using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.ObjectModel;

namespace GrahamThomson.Xna.Common.Particles
{
    public interface IParticleManager : IGameComponent
    {
        ReadOnlyCollection<Emitter> Emitters { get; }

        void AddParticle(Particle particle);
        void AddShower(Shower shower);
        void AddParticleTexture(ParticleTexture particleTexture);
        void AddEmitter(Emitter emitter);
    }

    public class ParticleManager : DrawableGameComponent, IParticleManager
    {
        private List<Emitter> _Emitters = new List<Emitter>();
        public ReadOnlyCollection<Emitter> Emitters
        {
            get { return _Emitters.AsReadOnly(); }
        }

        private SpriteBatch _SpriteBatch;
        private Texture2D _TextureStandard;
        private Texture2D _TextureHeart;
        private Dictionary<string, ParticleTexture> _Textures = new Dictionary<string, ParticleTexture>();
        private List<Particle> _Particles = new List<Particle>();

        public ParticleManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IParticleManager), this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _SpriteBatch = new SpriteBatch(GraphicsDevice);

            _TextureStandard = Game.Content.Load<Texture2D>("Textures/Particle");
            _TextureHeart = Game.Content.Load<Texture2D>("Textures/Heart");

            AddParticleTexture(new ParticleTexture("Standard", _TextureStandard));
            AddParticleTexture(new ParticleTexture("Heart", _TextureHeart));
        }

        public void AddParticleTexture(ParticleTexture particleTexture)
        {
            _Textures.Add(particleTexture.TextureName, particleTexture);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Particle particle in _Particles)
            {
                particle.Update(gameTime);
            }

            for (int index = _Particles.Count - 1; index >= 0; --index)
            {
                if (_Particles[index].IsDead)
                {
                    _Particles.RemoveAt(index);
                }
            }

            foreach (Emitter emitter in Emitters)
            {
                emitter.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            _SpriteBatch.Begin();

            foreach (Particle particle in _Particles)
            {
                DrawParticle(particle);
            }

            _SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawParticle(Particle particle)
        {
            if (particle.Visble)
            {
                ParticleTexture particleTexture = _Textures[particle.TextureName];

                _SpriteBatch.Draw(
                    particleTexture.Texture,
                    particle.Position,
                    particleTexture.Source,
                    particle.Color,
                    particle.Rotation,
                    particleTexture.TextureHalfSize,
                    particle.Scale,
                    SpriteEffects.None,
                    0);
            }
        }

        public void AddParticle(Particle particle)
        {
            _Particles.Add(particle);
        }

        public void AddShower(Shower shower)
        {
            IEnumerable<Particle> particles = shower.CreateParticles();
            _Particles.AddRange(particles);
        }

        public void AddEmitter(Emitter emitter)
        {
            _Emitters.Add(emitter);
            emitter.IntervalExpired += new EventHandler(Emitter_IntervalExpired);
        }

        private void Emitter_IntervalExpired(object sender, EventArgs e)
        {
            AddShower(((Emitter)sender).Shower);
        }
    }
}
