using Microsoft.Xna.Framework;
using NetRunner.UI.Xna.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Visuals
{
    public class CorporationPassesVisual : Visual
    {
        public CorporationPassesVisual(Game game)
            : base(game)
        {
        }

        public override void Activate()
        {
            base.Activate();

            _SoundManager.Play(NetRunnerSound.Boop);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (DurationSinceActivation > TimeSpan.FromSeconds(0.5))
            {
                IsComplete = true;
            }
        }
    }
}
