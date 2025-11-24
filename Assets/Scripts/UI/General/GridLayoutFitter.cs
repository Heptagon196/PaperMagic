using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    public class GridLayoutFitter : MonoBehaviour
    {
        public RectTransform detectTransform;
        private void Start()
        {
            var layout = GetComponent<GridLayoutGroup>();
            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            if (detectTransform == null)
            {
                detectTransform = transform.parent.GetComponent<RectTransform>();
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(detectTransform);
            var width = detectTransform.rect.width - layout.padding.left - layout.padding.right + layout.spacing.x;
            layout.constraintCount = Mathf.FloorToInt(width / (layout.cellSize.x + layout.spacing.x));
        }
    }
}
