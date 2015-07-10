using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrahamThomson.Xna.Common.Particles.Profiles
{
    public class Wander : Profile<Vector2>
    {
        public override Vector2 CurrentValue { get; protected set; }

        public float Angle { get; private set; }
        public float InitialSpeed { get; private set; }
        public float Speed { get; private set; }

        private float _TimeUntilDirectionChange;

        public Wander(Particle particle, float initialSpeed)
            : base(particle)
        {
            InitialSpeed = initialSpeed;
            UpdateDirection();
        }

        private void UpdateDirection()
        {
            Angle = MathHelper.TwoPi * (float)Random.NextDouble();
            Speed = InitialSpeed * (float)Random.NextDouble();
            _TimeUntilDirectionChange = 1 + (5 * (float)Random.NextDouble());
        }
        
        public override void Update(GameTime gameTime)
        {
            _TimeUntilDirectionChange -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_TimeUntilDirectionChange < 0)
            {
                _TimeUntilDirectionChange = 0;
                UpdateDirection();
            }

            Speed *= 0.99f;

            float actualAngle = Angle + (float)Math.Sin(Particle.Age);

            CurrentValue = Speed * new Vector2((float)Math.Cos(actualAngle), (float)Math.Sin(actualAngle));
        }
    }
}
