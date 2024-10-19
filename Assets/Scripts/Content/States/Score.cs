using System;
using Core.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Content.States
{
    public class Score : BaseSingleDirectionalFiniteMachineState
    {
        private readonly Sprite[] _medalSprites;
        private readonly UIBaseScreen _uiScreen;
        
        public Score(UIGameScreen gameScreen, Func<bool> shouldEndState)
        {
            _uiScreen = gameScreen;
            ShouldEndState = shouldEndState;
            
            //TODO:
            _medalSprites = new[]
            {
                Resources.Load<Sprite>("Sprites/bronzemedal"),
                Resources.Load<Sprite>("Sprites/silvermedal"),
                Resources.Load<Sprite>("Sprites/goldmedal")
            };
        }
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            //TODO:
            var score = FlappyBirdGameData.GameScore;
            
            var resultText = _uiScreen.Get<Text>("TitleText");
            resultText.text = "Oof! You lost!";
            TextStyler.SetTextStyle(resultText, new TextStyle()
            {
                font = Resources.Load<Font>("Fonts/flappy"),
                fontSize = TextStyler.MIDDLE_FONT_SIZE,
                anchoredPosition = new Vector2(resultText.rectTransform.anchoredPosition.x, -64f),
                sizeDelta = new Vector2(resultText.rectTransform.sizeDelta.x, 35f)
            });
            
               
            _uiScreen.Get<Text>("MessageText").text = $"Score: {score}";
            _uiScreen.EnableObject<Text>("MessageText");    
            
            //TODO: Get msg from keycode?
            _uiScreen.Get<Text>("MessageText_2").text = $"Press Enter to Play Again!";
            _uiScreen.EnableObject<Text>("MessageText_2");
            
            _uiScreen.Get<Image>("MedalImage").color = score < 1 ? new Color(0, 0, 0, 0) : Color.white;
            _uiScreen.Get<Image>("MedalImage").sprite =
                _medalSprites[Math.Min(score / 5, Math.Max(_medalSprites.Length - 1, 0))];
            _uiScreen.EnableObject<Image>("MedalImage");
        }
        
        public override void OnStateExit() => _uiScreen.ClearAll();
    }
}