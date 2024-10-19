using Core;
using Core.Input;
using Core.Input.InputActions;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Content.Components
{
    public class BirdComponent : MonoBehaviour //, IComponentSetAble
    {
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpDeltaY;
        [Header("Input")]
        [SerializeField] private BaseInputAction[] _jumpActions;
        private float _deltaY;
        
#if UNITY_EDITOR
        public void SetComponents()
        {
            // Values
            _gravity = -0.1388889f;
            _jumpDeltaY = 0.0694445f;
            // Input
            // _inputComponent = FindObjectOfType<InputComponent>();
            _jumpActions = new[]
            {
                //TODO: Replace Resources.Load to AssetDatabase.search_something_idk
                Resources.Load<BaseInputAction>("Data/KeyboardJumpAction"),
                Resources.Load<BaseInputAction>("Data/MouseJumpAction")
            };
            // Sfx
            // GameObject.Find("@Sfx")?.TryGetComponent(out _soundComponent);
            // _jumpAudioClip = Resources.Load<AudioClip>("Sounds/jump");
            
            EditorUtility.SetDirty(this);
        }
#endif
        private void Awake()
        {
            Assert.IsFalse(_gravity >= 0);
            Assert.IsFalse(_jumpDeltaY <= 0);
            
            Assert.IsFalse(_jumpActions.Length == 0);
            foreach (var inputAction in _jumpActions) InputManager.Instance.BindAction(inputAction, AddMovementInput);
        }

        private void OnEnable()
        {
            Initialize();
            return;
            
            void Initialize()
            {
                transform.position = Vector3.zero;
                _deltaY = 0f;
            }
        }

        private void Update()
        {
            _deltaY += _gravity * GameInstance.GameDelta;
            if(GameInstance.IsGamePaused == false) transform.position += new Vector3(0,_deltaY,0);
        }

        private void AddMovementInput(InputValue inputValue)
        {
            if(gameObject.activeSelf) Jump();
            return;
            
            void Jump()
            {
                transform.position += new Vector3(0,_deltaY = _jumpDeltaY,0); 
                SoundManager.Instance.PlaySfx("jump");
            }
        } 
    }
}