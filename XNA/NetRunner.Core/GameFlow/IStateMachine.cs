using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public interface IStateMachine
    {
        string Description { get; }
        IEnumerable<Trigger> PermittedTriggers { get; }

        void Initialize();
        bool CanFire(Trigger trigger);
        void Fire(Trigger trigger);

        // The following methods will only be implemented if the state machine allows the relevant action.

        void ScoreAgenda(object cardIdentifier);
    }
}
