// 0.0.2
using System;
using UnityEngine;

namespace Core.Input.InputActions
{
    [CreateAssetMenu(fileName = "KeyboardSingleKeyInputAction", menuName = "InputActions/KeyboardSingleKeyInputAction", order = 1)]

    public class KeyboardSingleKeyInputAction : BaseInputAction
    {
        public KeyCode keyCode;

        public KeyboardSingleKeyInputAction(KeyCode keyCode) => this.keyCode = keyCode;
        
        public override bool IsActionInvoked() => (GameInstance.IsGamePaused && canBeInvokedDuringPause == false) == false && GetInputValue().BoolValue;

        public override InputValue GetInputValue() =>
            inputCondition switch
            {
                EInputCondition.Up => new InputValue { BoolValue = UnityEngine.Input.GetKeyUp(keyCode) },
                EInputCondition.Down => new InputValue { BoolValue = UnityEngine.Input.GetKeyDown(keyCode) },
                EInputCondition.Pressing => new InputValue { BoolValue = UnityEngine.Input.GetKey(keyCode) },
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}