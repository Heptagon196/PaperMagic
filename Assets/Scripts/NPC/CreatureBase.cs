using System;
using Controller;
using Quest;
using UnityEngine;
using UnityEngine.Serialization;
using XLua;

namespace NPC
{
    [Serializable, LuaCallCSharp]
    public enum CreatureFaction
    {
        Friendly, // 友好
        Hostile, // 敌对
        Neutral, // 中立
    }
    [LuaCallCSharp, Serializable]
    public abstract class CreatureBase : MonoBehaviour
    {
        public static int CreatureXidCounter = 0;
        public float maxHealthPoint;
        public float healthPoint;
        public CreatureFaction faction;
        public static int AllocCreatureXid()
        {
            return CreatureXidCounter++;
        }
        // 返回是否应用此伤害
        public bool CanDoDamageTo(CreatureBase source)
        {
            // 中立阵营互相可伤害，不同阵营可互相伤害
            return faction != source.faction || faction == CreatureFaction.Neutral;
        }
        public abstract void OnTakeDamage(float damage, CreatureBase source);
        public void TakeDamage(float damage, CreatureBase source)
        {
            if (!CanDoDamageTo(source))
            {
                return;
            }
            healthPoint = Mathf.Clamp(healthPoint - damage, 0, maxHealthPoint);
            OnTakeDamage(damage, source);
        }
    }
}