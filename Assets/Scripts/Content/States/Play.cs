using System;
using System.Collections.Generic;
using Content.Components;
using Core;
using Core.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Content.States
{
    public class Play : BaseSingleDirectionalFiniteMachineState
    {
        private readonly BirdComponent _birdComponent;
        private readonly List<PipePair> _pipePairs;
        private readonly UIBaseScreen _uiScreen;        
        private Transform _lastPipe;
        private Action<int> _onScoreChanged;
        private bool _didBirdCollided;
        public Play(UIBaseScreen screen, BirdComponent birdComponent, List<PipePair> pipePairs)
        {
            _uiScreen = screen;
            _birdComponent = birdComponent;
            _pipePairs = pipePairs;
            foreach (var pair in _pipePairs) pair.SetComponents();
        }
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            ShouldEndState = IsBirdInRightPlace;
            
            SetComponentsActive(false);            
            InitGame();
            SetComponentsActive(true);
            
            var scoreText = _uiScreen.Get<Text>("TitleText");
            _onScoreChanged += newScore => scoreText.text = $"{newScore:D2}";
            TextStyler.SetTextStyle(scoreText, new TextStyle()
            {
                font = Resources.Load<Font>("Fonts/flappy"),
                fontSize = TextStyler.MIDDLE_FONT_SIZE,
                anchoredPosition = new Vector2(scoreText.rectTransform.anchoredPosition.x, -8f),
                sizeDelta = new Vector2(scoreText.rectTransform.sizeDelta.x, 35f)
            });
            _onScoreChanged?.Invoke(FlappyBirdGameData.GameScore);
        }

        public override void OnStateExit()
        {
            _uiScreen.ClearAll();
            SetComponentsActive(false);
        }

        // public override void Tick() { }

        private bool IsBirdInRightPlace()
        {
            return _birdComponent.transform.position.y is > GameConst.WorldYMax or < GameConst.WorldYMin || _didBirdCollided;
            // return UnityEngine.Input.GetKey(KeyCode.Space);
        }
        
        private void InitGame()
        {
            _didBirdCollided = false;
            _lastPipe = null;
            foreach (var pair in _pipePairs) _lastPipe = pair.SetRandomPipePositions(pair, _lastPipe);
            
            FlappyBirdGameData.GameScore = 0;
        }
        
        private void SetComponentsActive(bool isActive)
        {
            _birdComponent.gameObject.SetActive(isActive);
            foreach (var pair in _pipePairs)
            {
                pair.gameObject.SetActive(isActive);
                if (isActive)
                {
                    pair.UpperPipeComponent.OnCollideWithBird += OnBirdHurt;
                    pair.LowerPipeComponent.OnCollideWithBird += OnBirdHurt;
                    pair.OnBirdPassedPipes += AddScore;
                }
                else
                {
                    pair.UpperPipeComponent.OnCollideWithBird -= OnBirdHurt;
                    pair.LowerPipeComponent.OnCollideWithBird -= OnBirdHurt;
                    pair.OnBirdPassedPipes -= AddScore;
                }
            }
            
        }
        
        private void OnBirdHurt()
        {
            SoundManager.Instance.PlaySfx("explosion");
            SoundManager.Instance.PlaySfx("hurt");
            _didBirdCollided = true;
        }
        
        private void AddScore()
        {
            FlappyBirdGameData.GameScore++;
            _onScoreChanged?.Invoke(FlappyBirdGameData.GameScore);
            SoundManager.Instance.PlaySfx("score");
        }
        
    }
}