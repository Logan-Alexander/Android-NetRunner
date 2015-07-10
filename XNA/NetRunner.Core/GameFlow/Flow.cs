using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    public class Flow
    {
        private Stack<StateMachineBase> _Stack = new Stack<StateMachineBase>();

        public Flow()
        {
            Add(new MainStateMachine(this));
        }

        public Flow(object serializedState)
        {
            //TODO: Implement this properly when the serialize method has been implemented.
            Flow stack = (Flow)serializedState;
            
            foreach (StateMachineBase item in stack._Stack.Reverse())
            {
                MainStateMachine mainStateMachine = item as MainStateMachine;
                if (mainStateMachine != null)
                {
                    _Stack.Push(new MainStateMachine(this, mainStateMachine.State));
                }

                TurnOrderStateMachine turnOrderStateMachine = item as TurnOrderStateMachine;
                if (turnOrderStateMachine != null)
                {
                    _Stack.Push(new TurnOrderStateMachine(this, turnOrderStateMachine.State));
                }

                PaidAbilityWindowStateMachine paidAbilityWindowStateMachine = item as PaidAbilityWindowStateMachine;
                if (paidAbilityWindowStateMachine != null)
                {
                    _Stack.Push(new PaidAbilityWindowStateMachine(
                        this,
                        paidAbilityWindowStateMachine.State,
                        paidAbilityWindowStateMachine.OpponentWillHaveChanceToRespond,
                        paidAbilityWindowStateMachine.Options));
                }
            }
        }

        public void Add(StateMachineBase stateMachine)
        {
            _Stack.Push(stateMachine);
            stateMachine.Start();
        }

        public void Complete()
        {
            _Stack.Pop();
            CurrentStateMachine.ContinueAfterChildCompletes();
        }

        public StateMachineBase CurrentStateMachine
        {
            get { return _Stack.Peek(); }
        }

        public bool CanFire(Trigger trigger)
        {
            return CurrentStateMachine.CanFire(trigger);
        }

        public void Fire(Trigger trigger)
        {
            CurrentStateMachine.Fire(trigger);
        }

        internal object Serialize()
        {
            // TODO: Create an object which represents the state of each state machine
            // in the stack so that it can be transported around.
            return this;
        }

        public override string ToString()
        {
            return string.Join(" > ", _Stack.Reverse().Select(s => s.Description));
        }
    }
}
