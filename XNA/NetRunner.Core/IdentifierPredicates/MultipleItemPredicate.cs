using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.IdentifierPredicates
{
    public abstract class MultipleItemPredicate<T>
    {
        public virtual void Resolve(GameContext context) { }
        public abstract IEnumerable<T> GetItems(GameContext context);
    }
}
