using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Spell
{
    public class ProjectilePool : MonoBehaviour
    {
        public GameObject prefab;
        private static ProjectilePool _instance = null;
        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        private static readonly List<GameObject> Pool = new();
        public static void CleanUp()
        {
            Pool.RemoveAll(obj => obj == null);
        }
        public static GameObject GetObject()
        {
            if (_instance == null)
            {
                return null;
            }
            foreach (var obj in Pool.Where(obj => obj != null && !obj.activeInHierarchy))
            {
                obj.SetActive(true);
                return obj;
            }
            var newObj = Instantiate(_instance.prefab);
            newObj.SetActive(true);
            Pool.Add(newObj);
            return newObj;
        }
    }
}