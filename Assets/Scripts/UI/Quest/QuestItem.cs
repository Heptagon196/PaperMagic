using System;
using Quest;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Quest
{
    public class QuestItem : MonoBehaviour
    {
        public Text displayText;
        public Transform container;
        public static string GenerateText(string title, string text, int depth, QuestStatus status, bool optional)
        {
            var space = "";
            var textString = "";
            for (int idx = 0; idx < depth; idx++)
            {
                space += '\t';
            }

            if (!string.IsNullOrEmpty(title))
            {
                textString += $"<size=24>{title}</size>\n\n";
            }
            textString += status switch {
                QuestStatus.NotCompleted => "<color=Black>☐ ",
                QuestStatus.Completed => "<color=Green>☑ ",
                QuestStatus.Failed => "<color=Red>☒ ",
                _ => ""
            };
            if (optional)
            {
                textString += "<color=Grey>[可选]</color> ";
            }
            return space + textString + text.Replace("\n", "\n" + space) + "</color>";
        }
        public void SetText(string title, string text, int depth, QuestStatus status, bool optional)
        {
            displayText.text = GenerateText(title, text, depth, status, optional);
        }
        public float CalcWidgetHeight()
        {
            var height = displayText.transform.parent.GetComponent<RectTransform>().rect.height;
            for (int childIdx = 0; childIdx < container.childCount; childIdx++)
            {
                var child = container.GetChild(childIdx);
                LayoutRebuilder.ForceRebuildLayoutImmediate(child.GetComponent<RectTransform>());
                if (child.gameObject.activeSelf)
                {
                    height += child.GetComponent<QuestItem>()?.CalcWidgetHeight() ?? 0;
                }
            }
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            return height;
        }
    }
}