using UnityEngine;

namespace Decorates
{
    public class ChildSpriteOutliner : MonoBehaviour
    {
        public float thickness = 0.1f;
        void Start()
        {
            var childTransform = transform.GetChild(0).transform;
            var targetSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();
            int[,] dxy =
            {
                { 1, 0 },
                { 0, 1 },
                { -1, 0 },
                { 0, -1 }
            };
            Material material = new Material(Shader.Find("Shader Graphs/WhiteShader"));
            for (int idx = 0; idx < 4; idx++)
            {
                var obj = new GameObject();
                var comp = obj.AddComponent<SpriteRenderer>();
                obj.transform.SetParent(transform);
                obj.transform.localPosition = childTransform.localPosition + new Vector3(dxy[idx, 0], dxy[idx, 1], 0) * thickness;
                
                comp.sprite = targetSpriteRenderer.sprite;
                comp.color = Color.white;
                comp.sortingOrder = targetSpriteRenderer.sortingOrder - 1;
                comp.material = material;
            }
        }
    }
}
