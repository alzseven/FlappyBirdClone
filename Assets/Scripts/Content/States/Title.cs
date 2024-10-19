using System;
using Core.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Content.States
{
    public class Title : BaseSingleDirectionalFiniteMachineState
    {
        private readonly UIBaseScreen _uiScreen;
        
        public Title(UIBaseScreen uiScreen, Func<bool> shouldEndState)
        {
            _uiScreen = uiScreen;
            ShouldEndState = shouldEndState;
        }
        
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            var titleText = _uiScreen.Get<Text>("TitleText");
            titleText.text = "Flappy Bird";
            TextStyler.SetTextStyle(titleText, new TextStyle()
            {
                font = Resources.Load<Font>("Fonts/flappy"),
                fontSize = TextStyler.MIDDLE_FONT_SIZE,
                anchoredPosition = new Vector2(titleText.rectTransform.anchoredPosition.x, -64f),
                sizeDelta = new Vector2(titleText.rectTransform.sizeDelta.x, 35f)
            });
            
            //TODO: Get msg from keycode?
            _uiScreen.Get<Text>("MessageText").text = "Press Enter";
            _uiScreen.EnableObject<Text>("MessageText");
        }

        public override void OnStateExit() => _uiScreen.ClearAll();
    }
}