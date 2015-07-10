using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrahamThomson.Xna.Common.Particles.Profiles
{
    public class Gravity : Profile<Vector2>
    {
        public override Vector2 CurrentValue { get; protected set; }
        public float Mass { get; private set; }

        public Gravity(Particle particle, float initialSpeed, int mass, int massVariance = 0)
            : base(particle)
        {
            CurrentValue = new Vector2(
                initialSpeed * ((float)Random.NextDouble() * 2 - 1),
                initialSpeed * ((float)Random.NextDouble() * 2 - 1));
            
            Mass = mass + (massVariance * (float)Random.NextDouble());
        }

        public override void Update(GameTime gameTime)
        {
            CurrentValue += new Vector2(0, 9.8f * Mass * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
