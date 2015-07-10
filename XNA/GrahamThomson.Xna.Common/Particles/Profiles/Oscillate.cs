using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrahamThomson.Xna.Common.Particles.Profiles
{
    /// <summary>
    /// A particle Rotation profile.
    /// </summary>
    public class Oscillate : Profile<float>
    {
        public override float CurrentValue { get; protected set; }
        public float MaxAngle { get; private set; }
        public float Speed { get; private set; }
        public float Friction { get; private set; }

        private double _Offset;

        public Oscillate(Particle particle, float maxAngle, float speed, float friction = 1f)
            : base(particle)
        {
            MaxAngle = maxAngle;
            Speed = speed;
            Friction = friction;
            _Offset = Math.PI * 2 * Random.NextDouble();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CurrentValue = MaxAngle * (float)Math.Sin(_Offset + (Particle.Age * Speed));
            MaxAngle *= Friction;
        }
    }
}
