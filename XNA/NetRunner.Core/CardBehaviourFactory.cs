using NetRunner.Core.Conditions;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Selectors;
using NetRunner.Core.InstantEffects;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetRunner.Core.Corporation;
using NetRunner.Core.Runner;
using NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki;
using NetRunner.Core.CardDefinitions.CoreSet.CorpNeutral;
using NetRunner.Core.CardDefinitions.CoreSet.RunnerNeural;
using System.Reflection;

namespace NetRunner.Core
{
    /// <summary>
    /// This factory uses reflection to find all classes representing card behaviours by
    /// looking for the CardBehaviourIDAttribute.
    /// The factory allows card behaviours to be created from their name or ID and attached
    /// to a physical card.
    /// </summary>
    public class CardBehaviourFactory
    {
        private static object _Lock = new object();
        private static CardBehaviourFactory _Instance;
        public static CardBehaviourFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new CardBehaviourFactory();
                        }
                    }
                }

                return _Instance;
            }
        }

        private bool _DictionariesLazyLoaded;
        private Dictionary<CardBehaviourID, Type> _CardTypesByID;
        private Dictionary<string, Type> _CardTypesByTitle;

        public CardBehaviour Create(Card card, CardBehaviourID cardBehaviourID)
        {
            EnsureDictionariesPopulated();

            Type type = _CardTypesByID[cardBehaviourID];

            return (CardBehaviour)Activator.CreateInstance(type, new object[] { card });
        }

        public CardBehaviour Create(Card card, string title)
        {
            EnsureDictionariesPopulated();

            Type type = _CardTypesByTitle[title];

            return (CardBehaviour)Activator.CreateInstance(type, new object[] { card });
        }

        private void EnsureDictionariesPopulated()
        {
            if (!_DictionariesLazyLoaded)
            {
                Dictionary<CardBehaviourID, Type> cardTypesByID = new Dictionary<CardBehaviourID, Type>();
                Dictionary<string, Type> cardTypesByTitle = new Dictionary<string, Type>();

                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        CardBehaviourIDAttribute[] cardBehaviourIDAttributes =
                            (CardBehaviourIDAttribute[])type.GetCustomAttributes(typeof(CardBehaviourIDAttribute), false);

                        if (cardBehaviourIDAttributes.Length == 1)
                        {
                            CardBehaviourIDAttribute cardBehaviourIDAttribute = cardBehaviourIDAttributes[0];
                            cardTypesByID.Add(cardBehaviourIDAttribute.ID, type);
                            cardTypesByTitle.Add(cardBehaviourIDAttribute.Title, type);
                        }
                    }
                }

                _CardTypesByID = cardTypesByID;
                _CardTypesByTitle = cardTypesByTitle;

                _DictionariesLazyLoaded = true;
            }
        }
    }
}
