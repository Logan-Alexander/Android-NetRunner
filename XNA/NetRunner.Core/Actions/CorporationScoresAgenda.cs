using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationScoresAgenda : ActionBase
    {
        public object CardIdentifier { get; private set; }
        public string CardName { get; private set; }

        public CorporationScoresAgenda(object cardIdentifier)
        {
            CardIdentifier = cardIdentifier;
        }

        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.CorporationScoresAgenda);
        }

        protected override bool IsContextValidForCorporation(GameContext context)
        {
            if (!context.CardExists(CardIdentifier))
                return false;

            // TODO: Ensure the specified card is visible.

            // TODO: Ensure the card name provided is a match.

            // TODO: Ensure the card is an agenda.

            // TODO: Ensure that the agenda is advanced to the point where it can be scored.

            return true;
        }

        protected override bool IsContextValidForServer(GameContext context)
        {
            if (!context.CardExists(CardIdentifier))
                return false;

            // TODO: Ensure the specified card is visible to the corporation.

            // TODO: Ensure the card name provided is a match.

            // TODO: Ensure the card is an agenda.

            // TODO: Ensure that the agenda is advanced to the point where it can be scored.

            return true;
        }

        public override void ApplyToRunner(GameContext context, Flow flow)
        {
            context.IdentifityCard(CardIdentifier, CardName);
            base.ApplyToRunner(context, flow);
        }
        
        protected override void ApplyToAll(GameContext context, GameFlow.Flow flow)
        {
            // TODO: Move the agenda from the remote server to the corporation's score area.
            // TODO: Check for game end condition.

            PaidAbilityWindowStateMachine stateMachine = flow.CurrentStateMachine as PaidAbilityWindowStateMachine;
            stateMachine.CorporationScoresAgenda(CardIdentifier);
        }

        public override void AddInformationForRunner()
        {
            //TODO: Add the name of the card for the runner.
            CardName = "TODO";
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationScoresAgenda(this.CardIdentifier);
        }

        public override ActionBase Clone()
        {
            CorporationScoresAgenda clone = (CorporationScoresAgenda)base.Clone();
            clone.CardName = this.CardName;
            return clone;
        }
    }
}
