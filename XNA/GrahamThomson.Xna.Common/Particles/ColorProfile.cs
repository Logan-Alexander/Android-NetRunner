using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrahamThomson.Xna.Common.Particles
{
    public abstract class Profile
    {
        protected static readonly Random Random = new Random();

        public Particle Particle { get; private set; }

        public Profile(Particle particle)
        {
            Particle = particle;
        }
    }

    public abstract class Profile<T> : Profile
    {
        public Profile(Particle particle)
            : base(particle)
        {
        }

        public abstract T CurrentValue { get; protected set; }

        public abstract void Update(GameTime gameTime);
    }
}
