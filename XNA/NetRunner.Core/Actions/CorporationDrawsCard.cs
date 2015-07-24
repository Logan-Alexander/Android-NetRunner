using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationDrawsCard : ActionBase
    {
        /// <summary>
        /// The ID of the card that was drawn.
        /// </summary>
        public CardBehaviourID CardBehaviourID { get; private set; }

        public CorporationDrawsCard()
        {
            DeferExecution = true;
        }

        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.CorporationCardDrawn);
        }

        public override void ApplyToCorporation(GameContext context, Flow flow)
        {
            Card card = context.ResearchAndDevelopment.DrawPile.Peek();
            card.IdentifyCard(CardBehaviourID);
            
            base.ApplyToCorporation(context, flow);
        }

        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            Card card = context.ResearchAndDevelopment.DrawPile.Pop();
            card.KnownToCorporation = true;
            context.HeadQuarters.Hand.Add(card);

            flow.Fire(Trigger.CorporationCardDrawn);

            base.ApplyToAll(context, flow);
        }

        public override void AddInformationForCorporation(GameContext context, Flow flow)
        {
            Card card = context.ResearchAndDevelopment.DrawPile.Peek();
            CardBehaviourID = card.Behaviour.CardBehaviourID; 
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationDrawsCard();
        }

        public override ActionBase Clone()
        {
            CorporationDrawsCard clone = (CorporationDrawsCard)base.Clone();
            clone.CardBehaviourID = this.CardBehaviourID;
            return clone;
        }
    }
}
