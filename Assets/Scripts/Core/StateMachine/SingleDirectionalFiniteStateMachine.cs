namespace Core.StateMachine
{
    public class SingleDirectionalFiniteStateMachine
    {
        private ISingleDirectionalFiniteMachineState _currentMachineState;
        
        public void SetState(ISingleDirectionalFiniteMachineState newState)
        {
            if (ReferenceEquals(newState, _currentMachineState)) return;
            
            _currentMachineState?.OnStateExit();
            _currentMachineState = newState;
            _currentMachineState.OnStateEnter();
        }
        
        public void Tick()
        {
            if (_currentMachineState == null) return;
            
            if(_currentMachineState.ShouldEndState())
                SetState(_currentMachineState.NextState);
            
            _currentMachineState?.Tick();
        }
    }
}