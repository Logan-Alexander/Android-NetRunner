using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GrahamThomson.Xna.Common.Particles.Profiles;

namespace GrahamThomson.Xna.Common.Particles.Showers
{
    public class PinkLadyShower : Shower
    {
        public Vector2 Position { get; private set; }

        public PinkLadyShower(Vector2 position)
        {
            Position = position;
        }

        public override IEnumerable<Particle> CreateParticles()
        {
            for (int index = 0; index < 100; ++index)
            {
                yield return new PinkLady1(Position);
            }
            for (int index = 0; index < 3; ++index)
            {
                yield return new PinkLady2(Position);
            }
        }

        internal class PinkLady1 : Particle
        {
            public PinkLady1(Vector2 position)
                : base(position, "Standard", 0, 5 * (float)Random.NextDouble())
            {
                _ColorProfile = new Fade(this, new Color(1, 0, 0), new Color(1, 1, 1));
                _VelocityProfile = new Gravity(this, 150, 20, 20);
                _ScaleProfile = new Fixed<float>(this, 0.5f * (float)Random.NextDouble());
                _RotationProfile = new Fixed<float>(this, 0);
            }
        }

        internal class PinkLady2 : Particle
        {
            public PinkLady2(Vector2 position)
                : base(position, "Heart", 0, 15f * (float)Random.NextDouble())
            {
                _ColorProfile = new Fade(this, Color.Red);
                _VelocityProfile = new Gravity(this, 50, -20, -20);
                _ScaleProfile = new Fixed<float>(this, 0.25f + 0.25f * (float)Random.NextDouble());
                _RotationProfile = new Oscillate(this, MathHelper.ToRadians(30), 5, 0.99f);
            }
        }
    }
}
