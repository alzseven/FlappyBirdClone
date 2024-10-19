using System;
using Core;
using Core.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Content.States
{
    public class CountDown : BaseSingleDirectionalFiniteMachineState
    {
        private readonly int _countDownSeconds;
        private readonly float _countDownTime;

        private float _countdownTime;
        private int _count;
        private float _timer;
        
        event Action<int> onCountChanged;

        private readonly UIBaseScreen _uiScreen;
        
        public CountDown(UIBaseScreen uiScreen, int countDownSeconds, float countDownTime)
        {
            _uiScreen = uiScreen;
            _countDownSeconds = countDownSeconds;
            _countDownTime = countDownTime;
            ShouldEndState = IsCountDownFinished;
        }
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            _count = _countDownSeconds;
            _countdownTime = _countDownTime;
            _timer = 0f;
            
            var cntText = _uiScreen.Get<Text>("TitleText");
            
            TextStyler.SetTextStyle(cntText, new TextStyle()
            { 
                font = Resources.Load<Font>("Fonts/font"),
                fontSize = TextStyler.LARGE_FONT_SIZE,
                anchoredPosition = new Vector2(cntText.rectTransform.anchoredPosition.x, -120f),
                sizeDelta = new Vector2(cntText.rectTransform.sizeDelta.x, 70)
            });
            onCountChanged = newCount => cntText.text = newCount.ToString();
            onCountChanged?.Invoke(_count);
            
            cntText.text = _count.ToString();
        }
        
        public override void Tick()
        {
            _timer += GameInstance.GameDelta;

            if (_timer > _countdownTime)
            {
                _timer %= _countdownTime;
                _count--;
                onCountChanged?.Invoke(_count);
            }

        }

        public override void OnStateExit()
        {
            onCountChanged = null;
            _uiScreen.ClearAll();
        }

        private bool IsCountDownFinished() => _count == 0;
    }
}