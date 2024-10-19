using System;
using System.Collections.Generic;
using Core.Input.InputActions;
using UnityEngine;

namespace Core.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance
        {
            get
            {
                if(_instance == null) Init();
                return _instance;
            }
        }
        private static InputManager _instance;

        private static void Init()
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@Input");
                if (go == null)
                {
                    go = new GameObject { name = "@Input" };
                    go.AddComponent<InputManager>();
                }

                DontDestroyOnLoad(go);
                _instance = go.GetComponent<InputManager>();
            }		
        }
        
        //TODO: Load actions how?
        [SerializeField] private List<BaseInputAction> _inputActions;
        private Dictionary<BaseInputAction, Action<InputValue>> _inputActionMapping = new();
        
        private void Awake()
        {
            Init();
            // if (_instance == null) _instance = this;
            // else Destroy(gameObject);
        }

        public bool CheckActionInvoked(string nameToFind) => GetInputActionByName(nameToFind).IsActionInvoked();

        public BaseInputAction GetInputActionByName(string nameToFind)
        {
            var action = _inputActions.Find(action => action.name == nameToFind);
            if (action)
                return action;
            throw new Exception($"No inputAction named {nameToFind}");
        }
        
        public void BindAction(BaseInputAction inputAction, Action<InputValue> callback)
        {
            //Valid input check?
            //TODO:
            if (_inputActions.Contains(inputAction) == false)
            {
                // _inputActions.Add(action);
                throw new ArgumentException();
            }

            if (_inputActionMapping.TryAdd(inputAction, callback) == false)
            {
                _inputActionMapping[inputAction] += callback;
            }
        }
        
        private void Update()
        {
            foreach (var action in _inputActions)
            {
                if (action.IsActionInvoked() && 
                    (GameInstance.IsGamePaused && action.canBeInvokedDuringPause == false) == false) 
                    if(_inputActionMapping.ContainsKey(action))
                        _inputActionMapping[action]?.Invoke(action.GetInputValue());
            }
        }
    }
}