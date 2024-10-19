namespace Core.StateMachine
{
    public interface IState
    {
        void OnStateEnter();
        
        void OnStateExit();
        
        void Tick();
    }
}