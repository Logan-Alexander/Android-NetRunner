using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    /// <summary>
    /// Each card behaviour can be identified by the Card Set and an ID.
    /// </summary>
    public struct CardBehaviourID
    {
        public CardSet CardSet { get; set; }
        public int ID { get; set; }

        public CardBehaviourID(CardSet cardSet, int id)
            : this()
        {
            CardSet = cardSet;
            ID = id;
        }

        #region Equals

        public override bool Equals(object obj)
        {
            if (!(obj is CardBehaviourID))
                return false;

            CardBehaviourID other = (CardBehaviourID)obj;

            return other.CardSet == this.CardSet
                && other.ID == this.ID;
        }

        public override int GetHashCode()
        {
            return CardSet.GetHashCode() ^ ID.GetHashCode();
        }

        public static bool operator==(CardBehaviourID a, CardBehaviourID b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(CardBehaviourID a, CardBehaviourID b)
        {
            return !(a == b);
        }

        #endregion
    }
}
