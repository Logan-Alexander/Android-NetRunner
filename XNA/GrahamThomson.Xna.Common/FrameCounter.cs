using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GrahamThomson.Xna.Common
{
    public interface IFrameCounter
    {
        /// <summary>
        /// Gets the number of times the Update method has been called in the last second.
        /// </summary>
        int UpdatesPerSecond { get; }

        /// <summary>
        /// Gets the number of times the Draw method has been called in the last second.
        /// </summary>
        int RendersPerSecond { get; }
    }

    public class FrameCounter : DrawableGameComponent, IFrameCounter
    {
        /// <summary>
        /// Gets the number of times the Update method has been called in the last second.
        /// </summary>
        public int UpdatesPerSecond { get; private set; }

        /// <summary>
        /// Gets the number of times the Draw method has been called in the last second.
        /// </summary>
        public int RendersPerSecond { get; private set; }

        private Stopwatch _StopWatch;
        private int _UpdateCount;
        private int _RenderCount;

        public FrameCounter(Game game)
            : base (game)
        {
            Game.Services.AddService(typeof(IFrameCounter), this);
            Game.Components.Add(this);
            
            _StopWatch = new Stopwatch();
            _StopWatch.Start();
        }

        public override void Update(GameTime gameTime)
        {
            ++_UpdateCount;
            CheckStopwatch();
        }

        public override void Draw(GameTime gameTime)
        {
            ++_RenderCount;
            CheckStopwatch();
        }

        private void CheckStopwatch()
        {
            if (_StopWatch.ElapsedMilliseconds > 1000)
            {
                _StopWatch.Restart();
                RendersPerSecond = _RenderCount;
                UpdatesPerSecond = _UpdateCount;
                _RenderCount = 0;
                _UpdateCount = 0;
            }
        }
    }
}
