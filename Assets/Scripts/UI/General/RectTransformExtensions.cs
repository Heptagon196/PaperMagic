using UnityEngine;

namespace UI.General
{
    public static class RectTransformExtensions
    {
        public static void CopyRectTransformFrom(this RectTransform rectTransform, RectTransform other)
        {
            rectTransform.localScale = other.localScale;
            rectTransform.sizeDelta = other.sizeDelta;
            rectTransform.anchorMin = other.anchorMin;
            rectTransform.anchorMax = other.anchorMax;
            rectTransform.pivot = other.pivot;
            rectTransform.anchoredPosition = other.anchoredPosition;
            rectTransform.position = other.position;
        }
    }
}