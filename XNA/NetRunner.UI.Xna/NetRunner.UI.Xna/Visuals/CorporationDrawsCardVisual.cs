﻿using Microsoft.Xna.Framework;
using NetRunner.Core;
using NetRunner.UI.Xna.Components;
using NetRunner.UI.Xna.Visuals.CardAnimations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Visuals
{
    public class CorporationDrawsCardVisual : Visual
    {
        private Card _Card;

        public CorporationDrawsCardVisual(Game game, Card card)
            : base(game)
        {
            _Card = card;
        }

        public override void Activate()
        {
            base.Activate();

            // TODO

            _SoundManager.Play(NetRunnerSound.Boop);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (DurationSinceActivation > TimeSpan.FromSeconds(0.75))
            {
                IsComplete = true;
            }
        }
    }
}
