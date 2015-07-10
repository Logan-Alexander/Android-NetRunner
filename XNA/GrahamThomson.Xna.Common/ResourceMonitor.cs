using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GrahamThomson.Xna.Common
{
    public interface IResourceMonitor
    {
        /// <summary>
        /// Gets the working set memory for the game.
        /// </summary>
        long PrivateBytes { get; }
    }
    
    public class ResourceMonitor : GameComponent, IResourceMonitor
    {
        /// <summary>
        /// Gets the working set memory for the game.
        /// </summary>
        public long PrivateBytes { get; private set; }

        /// <summary>
        /// The time interval between updates to the statistics.
        /// </summary>
        public float RefreshInterval { get; set; }

        private float _TimeToRefresh;

        public ResourceMonitor(Game game, float refreshInterval = 1)
            : base(game)
        {
            RefreshInterval = refreshInterval;
            Game.Components.Add(this);
            Game.Services.AddService(typeof(IResourceMonitor), this);
        }

        public override void Update(GameTime gameTime)
        {
            _TimeToRefresh -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_TimeToRefresh <= 0)
            {
                _TimeToRefresh = RefreshInterval;
                RefreshStats();
            }
        }

        private void RefreshStats()
        {
            Process process = Process.GetCurrentProcess();
            PrivateBytes = process.WorkingSet64;
        }
    }
}
