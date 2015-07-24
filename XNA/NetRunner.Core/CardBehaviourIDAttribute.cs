using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    /// <summary>
    /// This attribute is used to identify classes representing card behaviours.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class CardBehaviourIDAttribute : Attribute
    {
        readonly string _Title;
        public string Title
        {
            get { return _Title; }
        }

        readonly CardBehaviourID _ID;
        public CardBehaviourID ID
        {
            get { return _ID; }
        }

        public CardBehaviourIDAttribute(CardSet cardSet, int id, string title)
        {
            _ID = new CardBehaviourID(cardSet, id);
            _Title = title;
        }
    }
}
