using System;
using Controller;
using Quest;
using UnityEngine;
using UnityEngine.Serialization;
using XLua;

namespace NPC
{
    [LuaCallCSharp, Serializable]
    public abstract class CreatureBase : MonoBehaviour
    {
        public float maxHealthPoint;
        public float healthPoint;
        public abstract void OnTakeDamage(float damage, CreatureBase source);
        public void TakeDamage(float damage, CreatureBase source)
        {
            healthPoint -= damage;
            healthPoint = Mathf.Clamp(healthPoint, 0, maxHealthPoint);
            OnTakeDamage(damage, source);
        }
    }
}