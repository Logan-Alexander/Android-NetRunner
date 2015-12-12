using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public abstract class StateMachineBase
    {
        public Flow GameFlow { get; private set; }

        public StateMachineBase(Flow gameFlow)
        {
            GameFlow = gameFlow;
        }

        internal abstract void ContinueAfterChildCompletes();

        public abstract bool CanFire(Trigger trigger);

        public abstract void Fire(Trigger trigger);

        public virtual void Fire(Trigger trigger, object arg)
        {
            throw new NotSupportedException();
        }

        internal virtual void Start()
        {
        }

        public abstract string Description { get; }

        public abstract IEnumerable<Trigger> PermittedTriggers { get; }
    }
}
