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
        /// The name of the card that was drawn.
        /// </summary>
        public string CardName { get; private set; }

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
            // TODO: Identify the card: context.IdentifityCard(TopCardOfR&D, CardName);
            base.ApplyToCorporation(context, flow);
        }

        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            // TODO: Move the top card of R&D to HQ.
            flow.Fire(Trigger.CorporationCardDrawn);
        }

        public override void AddInformationForCorporation()
        {
            // TODO: Set this to the top card of R&D.
            CardName = "TODO"; 
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationDrawsCard();
        }

        public override ActionBase Clone()
        {
            CorporationDrawsCard clone = (CorporationDrawsCard)base.Clone();
            clone.CardName = this.CardName;
            return clone;
        }
    }
}
