using System;
using System.Collections;
using Backpack;
using Equipment;
using NPC;
using PMLua.Export;
using Quest;
using Spell;
using UI.General;
using UnityEngine;
using XLua;
using Object = UnityEngine.Object;

namespace PMLua
{
    [LuaCallCSharp]
    public class PaperMagicLuaHelper
    {
        public BackpackLua Backpack = new();
        public ChatLua Chat = new();
        public CreatureLua Creature = new();
        public EffectLua Effect = new();
        public PlayerLua Player = new();
        public ProjectileLua Projectile = new();
        public QuestLua Quest = new();
        public void OnInit()
        {
            LuaManager.Env.Global.Set("PM", this);
            QuestManager.ReadAllQuestsInfo();
            SpellManager.RegisterAllSpells();
            SpellEffectManager.RegisterAllSpellEffects();
            EquipmentManager.RegisterAllEquipmentInfo();
            NormalItemManager.RegisterAllNormalItems();
            CreatureManager.RegisterAllCreatureInfo();
        }
        public bool IsValid(Object unityObject)
        {
            return unityObject != null;
        }
        public void Log(string msg)
        {
            Debug.Log(msg);
        }
        public void FloatMsg(string msg)
        {
            UIFunctions.Instance.ShowFloatTip(msg);
        }
        private static IEnumerator _StartTimer(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }
        public void StartTimer(float seconds, Action callBack)
        {
            LuaManager.Instance.StartCoroutine(_StartTimer(seconds, callBack));
        }
        public Vector3 RotateVec(Vector3 vec, float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward) * vec;
        }
    }
}