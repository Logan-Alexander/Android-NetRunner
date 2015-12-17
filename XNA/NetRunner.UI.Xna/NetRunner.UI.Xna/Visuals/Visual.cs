using Microsoft.Xna.Framework;
using NetRunner.UI.Xna.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Visuals
{
    public abstract class Visual
    {
        private Game _Game;
        protected INetRunnerSoundManager _SoundManager;

        public bool IsActive { get; private set; }

        protected TimeSpan DurationSinceActivation { get; private set; }

        public Visual(Game game)
        {
            _Game = game;
            _SoundManager = (INetRunnerSoundManager)game.Services.GetService(typeof(INetRunnerSoundManager));
        }

        public virtual void Activate()
        {
            IsActive = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                DurationSinceActivation += gameTime.ElapsedGameTime;
            }
        }

        public bool IsComplete { get; protected set; }
    }
}
