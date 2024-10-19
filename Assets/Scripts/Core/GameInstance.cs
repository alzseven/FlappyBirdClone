using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core
{
    public class GameInstance : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate = 60;
    
        public static float GameDelta;

        private static bool _isGamePaused;
        public static bool IsGamePaused
        {
            get => _isGamePaused;
            set
            {
                _isGamePaused = value;
                OnGamePauseChanged?.Invoke(_isGamePaused);
            }
        }
        public static Action<bool> OnGamePauseChanged;
        
        private void Awake() => Application.targetFrameRate = targetFrameRate;

        private void Update()
        {
            GameDelta = Time.deltaTime * (_isGamePaused ? 0 : 1);
        
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}
