using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrahamThomson.Xna.Common.Particles.Profiles
{
    public class Fixed<T> : Profile<T>
    {
        public override T CurrentValue { get; protected set; }

        public Fixed(Particle particle, T value)
            : base(particle)
        {
            CurrentValue = value;
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
