using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GrahamThomson.Xna.Common.Particles
{
    public abstract class Particle
    {
        public Vector2 Position { get; protected set; }

        protected Profile<Vector2> _VelocityProfile;
        public Vector2 Velocity
        {
            get
            {
                if (_VelocityProfile != null)
                {
                    return _VelocityProfile.CurrentValue;
                }
                else
                {
                    return Vector2.Zero;
                }
            }
        }

        protected Profile<Color> _ColorProfile;
        public Color Color
        {
            get
            {
                if (_ColorProfile != null)
                {
                    return _ColorProfile.CurrentValue;
                }
                else
                {
                    return Color.White;
                }
            }
        }

        protected Profile<float> _ScaleProfile;
        public float Scale
        {
            get
            {
                if (_ScaleProfile != null)
                {
                    return _ScaleProfile.CurrentValue;
                }
                else
                {
                    return 1;
                }
            }
        }

        protected Profile<float> _RotationProfile;
        public float Rotation
        {
            get
            {
                if (_RotationProfile != null)
                {
                    return _RotationProfile.CurrentValue;
                }
                else
                {
                    return 1;
                }
            }
        }

        public string TextureName { get; private set; }
        public float Delay { get; private set; }
        public float Age { get; private set; }
        public float? MaxAge { get; private set; }
        public bool IsDead { get; private set; }

        public bool Visble
        {
            get { return Delay == 0 && !IsDead; }
        }

        protected static readonly Random Random = new Random();

        public Particle(Vector2 position, string textureName, float delay, float? maxAge)
        {
            TextureName = textureName;
            Position = position;
            Age = 0;
            MaxAge = maxAge;
            Delay = delay;
        }

        public virtual void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (!IsDead)
            {
                if (Delay > 0)
                {
                    Delay -= elapsedTime;
                    if (Delay < 0)
                    {
                        Delay = 0;
                    }
                }
                else
                {
                    Age += elapsedTime;
                    Position += Velocity * elapsedTime;
                    
                    if (_ColorProfile != null)
                    {
                        _ColorProfile.Update(gameTime);
                    }
                    
                    if (_VelocityProfile != null)
                    {
                        _VelocityProfile.Update(gameTime);
                    }
                    
                    if (_ScaleProfile != null)
                    {
                        _ScaleProfile.Update(gameTime);
                    }
                    
                    if (_RotationProfile != null)
                    {
                        _RotationProfile.Update(gameTime);
                    }

                    if (MaxAge != null && Age >= MaxAge.Value)
                    {
                        Kill();
                    }
                }
            }
        }

        protected void Kill()
        {
            if (!IsDead)
            {
                IsDead = true;
            }
        }
    }
}
