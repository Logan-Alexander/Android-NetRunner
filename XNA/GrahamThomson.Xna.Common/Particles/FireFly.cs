using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GrahamThomson.Xna.Common.Particles.Profiles;

namespace GrahamThomson.Xna.Common.Particles
{
    public class FireFly : Particle
    {
        public FireFly(Vector2 position)
            : base(position, "Standard", 0, 100)
        {
            _ColorProfile = new Fixed<Color>(this, Color.Yellow * (float)Random.NextDouble());
            _RotationProfile = new Fixed<float>(this, 0);
            _ScaleProfile = new Fixed<float>(this, 0.05f);
            _VelocityProfile = new Wander(this, 50);
        }
    }
}
