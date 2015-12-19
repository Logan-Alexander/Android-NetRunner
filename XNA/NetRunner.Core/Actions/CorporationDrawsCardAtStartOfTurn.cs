using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationDrawsCardAtStartOfTurn : ActionBase
    {
        /// <summary>
        /// The ID of the card that was drawn.
        /// </summary>
        public CardBehaviourID CardBehaviourID { get; private set; }
        public Card Card { get; private set; }
        
        public CorporationDrawsCardAtStartOfTurn()
        {
            DeferExecution = true;
        }

        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.CorporationDrawsCardAtStartOfTurn);
        }

        public override void ApplyToCorporation(GameContext context, Flow flow)
        {
            Card card = context.ResearchAndDevelopment.DrawPile.Peek();
            card.IdentifyCard(CardBehaviourID);
            
            base.ApplyToCorporation(context, flow);
        }

        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            Card = context.ResearchAndDevelopment.DrawPile.Pop();
            Card.KnownToCorporation = true;
            context.HeadQuarters.Hand.Add(Card);

            flow.Fire(Trigger.CorporationDrawsCardAtStartOfTurn);

            base.ApplyToAll(context, flow);
        }

        public override void AddInformationForCorporation(GameContext context, Flow flow)
        {
            Card card = context.ResearchAndDevelopment.DrawPile.Peek();
            CardBehaviourID = card.Behaviour.CardBehaviourID; 
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationDrawsCardAtStartOfTurn();
        }

        public override ActionBase Clone()
        {
            CorporationDrawsCardAtStartOfTurn clone = (CorporationDrawsCardAtStartOfTurn)base.Clone();
            clone.CardBehaviourID = this.CardBehaviourID;
            return clone;
        }
    }
}
