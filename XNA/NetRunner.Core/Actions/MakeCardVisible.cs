using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    public class MakeCardVisible : ActionBase
    {
        public object CardIdentifier { get; private set; }
        public string CardName { get; private set; }

        public MakeCardVisible(object cardIdentifier, string cardName)
        {
            CardIdentifier = cardIdentifier;
            CardName = cardName;
        }

        public override bool IsValid(GameContext context, GameFlow.StateMachine stateMachine)
        {
            //TODO: Ensure cardIdentifier references a card that exists.
            //return CardIdentifier.CardExists(context);
            return true;
        }

        public override void Apply(GameContext context, GameFlow.StateMachine stateMachine)
        {
            //TODO: Attach the revealed behaviour to the card.
            //Card card = CardIdentifier.FindCard(context);
            //card.Behaviour = CardBehaviourFactory.Create(CardName);
        }

        protected override bool Equals(ActionBase otherAction)
        {
            // TODO: This action will never be compared as it originates from the hosted game
            // but we should still implement this :)
            return true;
        }
    }
}
