// 0.0.2
using System;
using UnityEngine;

namespace Core.Input.InputActions
{
    [CreateAssetMenu(fileName = "MouseInputAction", menuName = "InputActions/MouseInputAction", order = 0)]

    public class MouseInputAction : BaseInputAction
    {
        public int mouseButton;

        public MouseInputAction(int mouseButton) => this.mouseButton = mouseButton;

        public override bool IsActionInvoked() => (GameInstance.IsGamePaused && canBeInvokedDuringPause == false) == false && GetInputValue().BoolValue;

        public override InputValue GetInputValue() =>
            inputCondition switch
            {
                EInputCondition.Up => new InputValue { BoolValue = UnityEngine.Input.GetMouseButtonUp(mouseButton) },
                EInputCondition.Down => new InputValue { BoolValue = UnityEngine.Input.GetMouseButtonDown(mouseButton) },
                EInputCondition.Pressing => new InputValue { BoolValue = UnityEngine.Input.GetMouseButton(mouseButton) },
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
