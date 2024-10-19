using System;

namespace Core.StateMachine
{
    public abstract class BaseSingleDirectionalFiniteMachineState : ISingleDirectionalFiniteMachineState
    {
        public bool StateFinished { get; protected set;}

        public Func<bool> ShouldEndState { get; protected set; }

        //TODO: DI how?
        public ISingleDirectionalFiniteMachineState NextState { get; set; }

        public virtual void OnStateEnter() => StateFinished = false;

        public virtual void OnStateExit() { }

        public virtual void Tick() { }
    }
}