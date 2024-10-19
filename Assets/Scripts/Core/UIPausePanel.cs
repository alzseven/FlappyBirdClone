using System;
using UnityEngine;

namespace Core
{
    public class UIPausePanel : MonoBehaviour
    {
        private void Awake() => GameInstance.OnGamePauseChanged += TogglePanel;

        private void Start() => TogglePanel(GameInstance.IsGamePaused);
        // private void OnDestroy() => GameInstance.OnGamePauseChanged -= TogglePanel;
        
        private void TogglePanel(bool shouldBeActivated) => gameObject.SetActive(shouldBeActivated);
    }
}