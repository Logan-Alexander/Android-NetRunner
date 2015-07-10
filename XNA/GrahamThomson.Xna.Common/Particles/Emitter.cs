using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrahamThomson.Xna.Common.Particles
{
    public class Emitter
    {
        protected static readonly Random Random = new Random();

        public event EventHandler IntervalExpired;
        protected void OnIntervalExpired(EventArgs e)
        {
            EventHandler temp = IntervalExpired;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        public Shower Shower { get; private set; }
        public float MinInterval { get; private set; }
        public float MaxInterval { get; private set; }
        public bool Enabled { get; set; }

        private float _Interval;

        public Emitter(Shower shower, float minInterval, float maxInterval, bool enabled = true)
        {
            Shower = shower;
            MaxInterval = maxInterval;
            MinInterval = minInterval;
            Enabled = enabled;

            SetRandomInterval();
        }

        private void SetRandomInterval()
        {
            _Interval = MinInterval + ((MaxInterval - MinInterval) * (float)Random.NextDouble());
        }

        public void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                _Interval -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_Interval < 0)
                {
                    OnIntervalExpired(new EventArgs());
                    SetRandomInterval();
                }
            }
        }
    }
}
