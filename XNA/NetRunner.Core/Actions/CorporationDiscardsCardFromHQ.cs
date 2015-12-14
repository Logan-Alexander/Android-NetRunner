using NetRunner.Core.CardIdentifiers;
using NetRunner.Core.Corporation;
using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationDiscardsCardFromHQ : ActionBase
    {
        public HQCardIdentifier CardIdentifier { get; private set; }

        public CorporationDiscardsCardFromHQ(HQCardIdentifier cardIdentifier)
        {
            CardIdentifier = cardIdentifier;
        }

        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.CorporationDiscardsCardFromHQ);
        }

        protected override bool IsContextValidForCorporation(GameContext context)
        {
            Card card;

            // Ensure the card exists.
            if (!CardIdentifier.TryResolve(context, out card))
            {
                return false;
            }

            return true;
        }

        protected override bool IsContextValidForServer(GameContext context)
        {
            Card card;

            // Ensure the card exists.
            if (!CardIdentifier.TryResolve(context, out card))
            {
                return false;
            }

            return true;
        }

        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            Card card = CardIdentifier.Resolve(context);

            context.HeadQuarters.Hand.Remove(card);
            context.Archives.DiscardPile.Push(card);

            flow.CurrentStateMachine.Fire(Trigger.CorporationDiscardsCardFromHQ);
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationDiscardsCardFromHQ(this.CardIdentifier);
        }
    }
}
