using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrahamThomson.Xna.Common.Particles
{
    public abstract class Shower
    {
        protected static readonly Random Random = new Random();

        public abstract IEnumerable<Particle> CreateParticles();
    }
}
