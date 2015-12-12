using Microsoft.Xna.Framework;
using NetRunner.Core.Actions;
using NetRunner.UI.Xna.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    public class VisualManager : GameComponent
    {
        private ICorporationGameService _CorporationGameService;

        private Queue<Visual> _VisualQueue = new Queue<Visual>();

        public VisualManager(Game game)
            : base(game)
        {
            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            _CorporationGameService =
                (ICorporationGameService)Game.Services.GetService(typeof(ICorporationGameService));

            _CorporationGameService.CorporationGame.ActionTaken += CorporationGame_ActionTaken;
        }

        private void CorporationGame_ActionTaken(object sender, Core.Actions.ActionEventArgs e)
        {
            if (e.Action is CorporationPasses)
            {
                _VisualQueue.Enqueue(new CorporationPassesVisual(Game));
            }

            if (e.Action is RunnerPasses)
            {
                _VisualQueue.Enqueue(new RunnerPassesVisual(Game));
            }

            if (e.Action is CorporationDrawsCard)
            {
                CorporationDrawsCard action = (CorporationDrawsCard)e.Action;

                _VisualQueue.Enqueue(new CorporationDrawsCardVisual(Game, action.Card));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_VisualQueue.Count > 0)
            {
                Visual currentVisual = _VisualQueue.Peek();

                if (!currentVisual.IsActive)
                {
                    currentVisual.Activate();
                }

                currentVisual.Update(gameTime);

                if (currentVisual.IsComplete)
                {
                    _VisualQueue.Dequeue();
                }
            }
        }
    }
}
