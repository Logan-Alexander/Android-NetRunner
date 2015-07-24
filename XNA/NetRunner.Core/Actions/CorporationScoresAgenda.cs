using NetRunner.Core.CardIdentifiers;
using NetRunner.Core.Corporation;
using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class CorporationScoresAgenda : ActionBase
    {
        public AssetOrAgendaIdentifier CardIdentifier { get; private set; }
        public CardBehaviourID CardBehaviourID { get; private set; }

        public CorporationScoresAgenda(AssetOrAgendaIdentifier cardIdentifier)
        {
            CardIdentifier = cardIdentifier;
        }

        protected override bool IsFlowValid(Flow flow)
        {
            return flow.CanFire(Trigger.CorporationScoresAgenda);
        }

        protected override bool IsContextValidForCorporation(GameContext context)
        {
            Card card;

            // Ensure the card exists.
            if (!CardIdentifier.TryResolve(context, out card))
            {
                return false;
            }

            // TODO: Ensure the card is installed in a remote server.

            // Ensure the card is an agenda.
            AgendaCardBehaviour agenda = card.Behaviour as AgendaCardBehaviour;
            if (agenda == null)
            {
                return false;
            }

            // Ensure that the agenda is advanced to the point where it can be scored.
            if (card.HostedAgendaTokens < agenda.AdvancementRequirement)
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

            // TODO: Ensure the card is installed in a remote server.

            // Ensure the card is an agenda.
            AgendaCardBehaviour agenda = card.Behaviour as AgendaCardBehaviour;
            if (agenda == null)
            {
                return false;
            }

            // Ensure that the agenda is advanced to the point where it can be scored.
            if (card.HostedAgendaTokens < agenda.AdvancementRequirement)
            {
                return false;
            }

            return true;
        }

        public override void ApplyToRunner(GameContext context, Flow flow)
        {
            Card card = CardIdentifier.Resolve(context);
            card.IdentifyCard(CardBehaviourID);
            
            base.ApplyToRunner(context, flow);
        }
        
        protected override void ApplyToAll(GameContext context, Flow flow)
        {
            Card card = CardIdentifier.Resolve(context);
            card.KnownToCorporation = true;
            card.KnownToRunner = true;

            RemoteServer server = CardIdentifier.ResolveRemoteServer(context);
            server.AssetOrAgenda = null;
            ((AgendaCardBehaviour)card.Behaviour).Server = null;

            if (server.IsEmpty)
            {
                context.RemoteServers.Remove(server);
            }

            context.CorporationScoreArea.Agendas.Add(card);

            // TODO: Check for game end condition.

            PaidAbilityWindowStateMachine stateMachine = flow.CurrentStateMachine as PaidAbilityWindowStateMachine;
            stateMachine.CorporationScoresAgenda(CardIdentifier);
        }

        public override void AddInformationForRunner(GameContext context, Flow flow)
        {
            Card card = CardIdentifier.Resolve(context);
            CardBehaviourID = card.Behaviour.CardBehaviourID;
        }

        protected override ActionBase CreateInstanceForClone()
        {
            return new CorporationScoresAgenda(this.CardIdentifier);
        }

        public override ActionBase Clone()
        {
            CorporationScoresAgenda clone = (CorporationScoresAgenda)base.Clone();
            clone.CardBehaviourID = this.CardBehaviourID;
            return clone;
        }
    }
}
