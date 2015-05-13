using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Selectors
{
    public interface ISelector<out T>
    {
        void Resolve(GameContext context);
        bool IsResolved { get; }
        IEnumerable<T> Items { get; }
    }
}
