using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public abstract class CorporationFactionCardBehaviour : CorporationCardBehaviour
    {
        private int _BadPublicity;
        public int BadPublicity
        {
            get { return _BadPublicity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _BadPublicity = value;
            }
        }

        public List<PostTrigger> Triggers { get; private set; }

        public CorporationFactionCardBehaviour(Card card, CorporationFaction faction)
            : base(card, 0, faction)
        {
            Triggers = new List<PostTrigger>();
        }
    }
}
