using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Visuals.CardAnimations
{
    public class SimpleCardAnimation : ICardAnimation
    {
        public ICardLocation StartLocation { get; set; }
        public ICardLocation EndLocation { get; set; }
        public float Duration { get; set; }
        public float ElapsedTime { get; set; }

        public float Lambda
        {
            get
            {
                // Simple bezier easing function.
                float t = ElapsedTime / Duration;
                return (t * t) * (3.0f - 2.0f * t);
            }
        }

        public float OneMinusLambda
        {
            get { return 1 - Lambda; }
        }
        
        public ICardLocation Location
        {
            get
            {
                Vector3 position = (StartLocation.Position * OneMinusLambda) + (EndLocation.Position * Lambda);
                Vector3 forward = (StartLocation.Forward * OneMinusLambda) + (EndLocation.Forward * Lambda);
                Vector3 up = (StartLocation.Up * OneMinusLambda) + (EndLocation.Up * Lambda);
                
                return new StationaryCardLocation()
                {
                    Position = position,
                    Forward = forward,
                    Up = up
                };
            }
        }

        public bool IsComplete
        {
            get { return ElapsedTime >= Duration; }
        }

        public SimpleCardAnimation(ICardLocation startLocation, ICardLocation endLocation, float duration)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Duration = duration;
        }

        public void Update(GameTime gameTime)
        {
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (ElapsedTime > Duration)
            {
                ElapsedTime = Duration;
            }
        }
    }
}
