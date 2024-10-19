using UnityEngine.UI;

namespace Content
{
    public class TextStyler
    {
        //TODO:
        public const int LARGE_FONT_SIZE = 56;
        public const int MIDDLE_FONT_SIZE = 28;
        
        public static void SetTextStyle(Text target, TextStyle style)
        {
            target.gameObject.SetActive(false);
            target.font = style.font;
            target.fontSize = style.fontSize;
            target.rectTransform.anchoredPosition = style.anchoredPosition;
            target.rectTransform.sizeDelta = style.sizeDelta;
            target.gameObject.SetActive(true);
        }
    }
}