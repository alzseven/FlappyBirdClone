using System.Collections.Generic;
using Content.Components;
using Content.States;
using Core;
using Core.Input;
using Core.StateMachine;
using UnityEngine;

namespace Content
{
    public class FlappyBirdGameMode : MonoBehaviour
    {
        [Header("# CountDown")] 
        [SerializeField] private int _countDownSeconds;
        [SerializeField] private float _countDownTime;
        [Header("# Play")] 
        [SerializeField] private BirdComponent _birdComponent;
        [SerializeField] private List<PipePair> _pipePairs;
        [Header("# UI")] 
        [SerializeField] private UIGameScreen uiGameScreen;

        private readonly SingleDirectionalFiniteStateMachine _flappyBirdGameModeStateMachine = new();

        private void Awake()
        {
            uiGameScreen.ClearAll();
            _birdComponent.gameObject.SetActive(false);
            foreach(var pair in _pipePairs) pair.gameObject.SetActive(false);
        }

        private void Start()
        {
            SoundManager.Instance.PlayBackGroundMusic("marios_way");
            InputManager.Instance.BindAction(InputManager.Instance.GetInputActionByName("PauseAction"), PauseGame);
        
            var title = new Title(uiGameScreen, InputManager.Instance.GetInputActionByName("ChangeStateAction").IsActionInvoked);
            var countDown = new CountDown(uiGameScreen, _countDownSeconds, _countDownTime);
            var play = new Play(uiGameScreen, _birdComponent, _pipePairs);
            var score = new Score(uiGameScreen, InputManager.Instance.GetInputActionByName("ChangeStateAction").IsActionInvoked);
            //NextStates
            title.NextState = countDown;
            countDown.NextState = play;
            play.NextState = score;
            score.NextState = countDown;

            _flappyBirdGameModeStateMachine.SetState(title);
        }
    
        private void PauseGame(InputValue inputValue) => GameInstance.IsGamePaused = !GameInstance.IsGamePaused;

        private void Update() => _flappyBirdGameModeStateMachine?.Tick();
    }
}