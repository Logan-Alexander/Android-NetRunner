using Microsoft.Xna.Framework;
using NetRunner.Core.GameManagement;
using NetRunner.UI.Xna.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Visuals
{
    public class CorporationGameLoadedGameVisual : Visual
    {
        protected CorporationGame _CorporationGame;

        public CorporationGameLoadedGameVisual(Game game, CorporationGame corporationGame)
            : base(game)
        {
            _CorporationGame = corporationGame;
        }

        public override void Activate()
        {
            base.Activate();

            // TODO

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
