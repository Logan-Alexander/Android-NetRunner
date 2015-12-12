using Microsoft.Xna.Framework;
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

        private DrawableCard _DrawableCard;
        private SimpleCardAnimation _Animation;

        public CorporationDrawsCardVisual(Game game, Card card)
            : base(game)
        {
            _Card = card;
        }

        public override void Activate()
        {
            base.Activate();

            ICardLocation startLocation = new StationaryCardLocation()
            {
                Position = new Vector3(0.25f, 0, 0),
                Forward = new Vector3(0, -1, 0),
                Up = new Vector3(0, 0, -1)
            };

            ICardLocation endLocation = new StationaryCardLocation()
            {
                Position = new Vector3(0, 0.3f, 0.3f),
                Forward = new Vector3(0, 0.75f, 1),
                Up = new Vector3(0, 1, 0)
            };

            if (!_CardsUI.TryFindCard(_Card, out _DrawableCard))
            {
                _DrawableCard = _CardsUI.CreateCard(_Card, startLocation);
            }

            _Animation = new SimpleCardAnimation(startLocation, endLocation, 0.75f);
            _DrawableCard.Animation = _Animation;

            _SoundManager.Play(NetRunnerSound.Boop);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (_Animation.IsComplete)
            {
                IsComplete = true;
            }
        }
    }
}
