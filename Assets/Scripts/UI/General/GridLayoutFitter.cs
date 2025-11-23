using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    public class GridLayoutFitter : MonoBehaviour
    {
        private void Start()
        {
            var layout = GetComponent<GridLayoutGroup>();
            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            var parentRect = transform.parent.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
            var width = parentRect.rect.width - layout.padding.left - layout.padding.right + layout.spacing.x;
            layout.constraintCount = Mathf.FloorToInt(width / (layout.cellSize.x + layout.spacing.x));
        }
    }
}
