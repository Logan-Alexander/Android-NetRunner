using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public abstract class StateMachineBase<TState> : IStateMachine
    {
        private StateMachine<TState, Trigger> _Machine;

        protected Flow GameFlow { get; private set; }

        public TState State { get; private set; }

        public abstract string Description { get; }

        public IEnumerable<Trigger> PermittedTriggers
        {
            get { return _Machine.PermittedTriggers; }
        }

        public StateMachineBase(Flow gameFlow, TState state)
        {
            GameFlow = gameFlow;
            State = state;
        }

        public virtual void Initialize()
        {
            _Machine = new StateMachine<TState, Trigger>(() => State, s => State = s);
            ConfigureStateMachine(_Machine);
        }

        protected abstract void ConfigureStateMachine(StateMachine<TState, Trigger> machine);

        public bool CanFire(Trigger trigger)
        {
            return _Machine.CanFire(trigger);
        }

        public void Fire(Trigger trigger)
        {
            _Machine.Fire(trigger);
        }

        protected void Fire<TParam>(StateMachine<TState, Trigger>.TriggerWithParameters<TParam> trigger, TParam parameter)
        {
            Fire(trigger, parameter);
        }

        public virtual void ScoreAgenda(object cardIdentifier)
        {
            // Only state machines that permit this trigger will override this method.
            throw new NotSupportedException();
        }

        protected void AddChild(IStateMachine stateMachine)
        {
            GameFlow.Add(stateMachine);
        }
    }
}
