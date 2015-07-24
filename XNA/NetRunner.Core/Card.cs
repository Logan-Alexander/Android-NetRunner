using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace NetRunner.Core
{
    /// <summary>
    /// A card represents a physical card that exists in a game of Android Net Runner.
    /// If the card has not been identified, the behaviour will be null. For example, cards
    /// in the Runner's Grip are not identified to the corporation.
    /// </summary>
    public class Card
    {
        public PlayerType PlayerType { get; private set; }

        public bool KnownToCorporation { get; set; }
        public bool KnownToRunner { get; set; }

        public bool CardIsIdentified
        {
            get { return Behaviour != null;  }
        }

        public CardBehaviour Behaviour { get; private set; }

        private int _HostedAgendaTokens;
        public int HostedAgendaTokens
        {
            get { return _HostedAgendaTokens; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _HostedAgendaTokens = value;
            }
        }

        public bool HasHostedAgendaToken
        {
            get { return HostedAgendaTokens > 0; }
        }

        private int _HostedVirusTokens;
        public int HostedVirusTokens
        {
            get { return _HostedVirusTokens; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _HostedVirusTokens = value;
            }
        }

        public bool HasHostedVirusToken
        {
            get { return HostedVirusTokens > 0; }
        }

        public Card(PlayerType playerType)
        {
            PlayerType = playerType;
        }

        public void IdentifyCard(CardBehaviourID cardBehaviourID)
        {
            CardBehaviour behaviour = CardBehaviourFactory.Instance.Create(this, cardBehaviourID);

            EnsureIdentifiedBehaviourIsValid(behaviour);

            Behaviour = behaviour;
        }

        public void IdentifyCard(string title)
        {
            CardBehaviour behaviour = CardBehaviourFactory.Instance.Create(this, title);

            EnsureIdentifiedBehaviourIsValid(behaviour);

            Behaviour = behaviour;
        }

        private void EnsureIdentifiedBehaviourIsValid(CardBehaviour behaviour)
        {
            if (CardIsIdentified)
            {
                if (Behaviour.CardBehaviourID != behaviour.CardBehaviourID)
                {
                    throw new Exception("This card has already been identified with a different behaviour.");
                }
            }
            else
            {
                if (behaviour.PlayerType != PlayerType)
                {
                    throw new Exception("The behaviour attached to a card must match the player type of the card.");
                }
            }
        }
    }
}
