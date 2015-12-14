using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameFlow
{
    /// <summary>
    /// The Flow stores all details about where we are in the game lifecycle.
    /// This information is represented as a nested set (stack) of small workflows,
    /// each of which is a state machine.
    /// </summary>
    public class Flow
    {
        public GameContext Context { get; private set; }

        private Stack<IStateMachine> _Stack = new Stack<IStateMachine>();

        public Flow(GameContext context)
        {
            Context = context;
            Add(new MainStateMachine(this));
        }

        public Flow(GameContext context, SerializedFlow serializedFlow)
        {
            Context = context;

            //TODO: Implement this properly when the serialize method has been implemented.
            Flow stack = serializedFlow.Flow;
            
            foreach (IStateMachine item in stack._Stack.Reverse())
            {
                MainStateMachine mainStateMachine = item as MainStateMachine;
                if (mainStateMachine != null)
                {
                    IStateMachine stateMachine = new MainStateMachine(this, mainStateMachine.State);
                    stateMachine.Initialize();
                    _Stack.Push(stateMachine);
                }

                TurnOrderStateMachine turnOrderStateMachine = item as TurnOrderStateMachine;
                if (turnOrderStateMachine != null)
                {
                    IStateMachine stateMachine = new TurnOrderStateMachine(this, turnOrderStateMachine.State);
                    stateMachine.Initialize();
                    _Stack.Push(stateMachine);
                }

                PaidAbilityWindowStateMachine paidAbilityWindowStateMachine = item as PaidAbilityWindowStateMachine;
                if (paidAbilityWindowStateMachine != null)
                {
                    IStateMachine stateMachine = new PaidAbilityWindowStateMachine(
                        this,
                        paidAbilityWindowStateMachine.State,
                        paidAbilityWindowStateMachine.OpponentWillHaveChanceToRespond,
                        paidAbilityWindowStateMachine.Options);
                    
                    stateMachine.Initialize();
                    _Stack.Push(stateMachine);
                }

                CorporationActionsStateMachine corporationActionsStateMachine = item as CorporationActionsStateMachine;
                if (corporationActionsStateMachine != null)
                {
                    IStateMachine stateMachine = new CorporationActionsStateMachine(this, corporationActionsStateMachine.State);

                    stateMachine.Initialize();
                    _Stack.Push(stateMachine);
                }

                CorporationDiscardDownToMaxHandSizeStateMachine corporationDiscardDownToMaxHandSizeStateMachine = item as CorporationDiscardDownToMaxHandSizeStateMachine;
                if (corporationDiscardDownToMaxHandSizeStateMachine != null)
                {
                    IStateMachine stateMachine = new CorporationDiscardDownToMaxHandSizeStateMachine(this, corporationDiscardDownToMaxHandSizeStateMachine.State);

                    stateMachine.Initialize();
                    _Stack.Push(stateMachine);
                }
            }
        }

        public void Add(IStateMachine stateMachine)
        {
            _Stack.Push(stateMachine);
            stateMachine.Initialize();

            if (CurrentStateMachine.CanFire(Trigger.Auto))
            {
                CurrentStateMachine.Fire(Trigger.Auto);
            }
        }

        public void Complete()
        {
            _Stack.Pop();

            if (CurrentStateMachine.CanFire(Trigger.ChildStateMachineComplete))
            {
                CurrentStateMachine.Fire(Trigger.ChildStateMachineComplete);
            }
        }

        public IStateMachine CurrentStateMachine
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

        internal SerializedFlow Serialize()
        {
            // TODO: Create an object which represents the state of each state machine
            // in the stack so that it can be transported around.
            return new SerializedFlow(this);
        }

        public override string ToString()
        {
            return string.Join(" > ", _Stack.Reverse().Select(s => s.Description));
        }
    }
}
