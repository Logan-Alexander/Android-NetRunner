using NetRunner.Core.Corporation;
using NetRunner.Core.Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.ContinuousEffects
{
    public abstract class ContinuousEffect : Effect
    {
        public override void Execute(GameContext context)
        {
            context.ActiveContinuousEffects.Add(this);
        }

        public virtual void ModifyIceIntent(GameContext context, PieceOfIce pieceOfIce, ModifyIceIntent intent) { }

        public virtual void ModifyRezIntent(GameContext context, IRezableCard rezableCard, ModifyRezIntent intent) { }
    }
}
