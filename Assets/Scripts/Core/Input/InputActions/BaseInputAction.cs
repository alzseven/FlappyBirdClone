// 0.0.2
using UnityEngine;

namespace Core.Input.InputActions
{
    public abstract class BaseInputAction : ScriptableObject //, IInputAction<InputValue>
    {
        public EInputCondition inputCondition;
        public bool canBeInvokedDuringPause;
        
        public virtual bool IsActionInvoked() => default;

        public virtual InputValue GetInputValue() => new();
    }
}