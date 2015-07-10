using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrahamThomson.Xna.Common.Particles.Profiles
{
    public class Fade : Profile<Color>
    {
        public Color InitialColor { get; protected set; }
        public override Color CurrentValue { get; protected set; }

        public Fade(Particle particle)
            : base(particle)
        {
            float r = (float)Random.NextDouble();
            float g = (float)Random.NextDouble();
            float b = (float)Random.NextDouble();
            InitialColor = new Color(r, g, b);
        }

        public Fade(Particle particle, Color initialColor)
            : base(particle)
        {
            InitialColor = initialColor;
        }

        public Fade(Particle particle, Color initialColorMin, Color initialColorMax)
            : base(particle)
        {
            float rMin = initialColorMin.R;
            float gMin = initialColorMin.G;
            float bMin = initialColorMin.B;
            float aMin = initialColorMin.A;
            float rRange = initialColorMax.R - initialColorMin.R;
            float gRange = initialColorMax.G - initialColorMin.G;
            float bRange = initialColorMax.B - initialColorMin.B;
            float aRange = initialColorMax.A - initialColorMin.A;

            float r = rMin + rRange * (float)Random.NextDouble();
            float g = gMin + gRange * (float)Random.NextDouble();
            float b = bMin + bRange * (float)Random.NextDouble();
            float a = aMin + aRange * (float)Random.NextDouble();

            InitialColor = new Color(r, g, b, a);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentValue = InitialColor * (1 - (Particle.Age / Particle.MaxAge.Value));
        }
    }
}
