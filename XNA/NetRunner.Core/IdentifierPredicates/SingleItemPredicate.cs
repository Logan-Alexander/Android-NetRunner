using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.IdentifierPredicates
{
    public abstract class SingleItemPredicate<T>
    {
        public event EventHandler<GameContextEventArgs> ItemAcquired;
        protected void OnItemAcquired(GameContextEventArgs e)
        {
            EventHandler<GameContextEventArgs> temp = ItemAcquired;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        public virtual void Resolve(GameContext context) { }
        public abstract T GetItem(GameContext context);
    }
}
