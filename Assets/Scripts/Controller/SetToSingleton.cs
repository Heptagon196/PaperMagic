using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller
{
    public class SetToSingleton : MonoBehaviour
    {
        public MonoBehaviour singletonScript;
        private static readonly Dictionary<Type, SetToSingleton> InstanceList = new();
        private void Awake()
        {
            if (InstanceList.TryGetValue(singletonScript.GetType(), out var instance))
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            InstanceList.TryAdd(singletonScript.GetType(), this);
            DontDestroyOnLoad(gameObject);
        }
    }
}