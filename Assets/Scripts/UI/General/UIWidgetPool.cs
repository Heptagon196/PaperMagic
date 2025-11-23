using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.General
{
    public class UIWidgetPool : MonoBehaviour
    {
        public GameObject prefab;
        private readonly List<GameObject> _pool = new();
        public void CleanUp()
        {
            _pool.RemoveAll(obj => obj == null);
        }
        public GameObject GetObject()
        {
            foreach (var obj in _pool.Where(obj => obj != null && !obj.activeInHierarchy))
            {
                obj.SetActive(true);
                return obj;
            }
            var newObj = Instantiate(prefab, transform);
            newObj.SetActive(true);
            _pool.Add(newObj);
            return newObj;
        }
    }
}