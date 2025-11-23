using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    public class FloatTipWidget : MonoBehaviour
    {
        public Image tipBackground;
        public Text tipText;
        public void ShowTip(string text, Vector2 startPos, Vector2 endPos, float duration, float stayTime)
        {
            transform.SetAsLastSibling();
            tipText.text = text;
            var origColor = tipBackground.color;
            origColor.a = 0;
            tipBackground.color = origColor;
            var textColor = tipText.color;
            textColor.a = 0;
            tipText.color = textColor;
            transform.localPosition = startPos;
            tipBackground.DOFade(1, duration);
            tipText.DOFade(1, duration);
            transform.DOLocalMove(endPos, duration);
            transform.DOScale(1, duration + stayTime).OnComplete(() =>
            {
                tipBackground.DOFade(0, duration);
                tipText.DOFade(0, duration).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });
        }
    }
}