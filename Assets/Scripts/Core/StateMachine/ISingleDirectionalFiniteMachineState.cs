using System;

namespace Core.StateMachine
{
    public interface ISingleDirectionalFiniteMachineState : IState
    {
        public Func<bool> ShouldEndState { get; }
        
        //TODO: DI how?
        public ISingleDirectionalFiniteMachineState NextState { get; set; }
    }
}