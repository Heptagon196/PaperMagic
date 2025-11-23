using Backpack;
using Equipment;
using NPC;
using PMLua.Export;
using Quest;
using Spell;
using UnityEngine;
using XLua;

namespace PMLua
{
    [LuaCallCSharp]
    public class PaperMagicLuaHelper
    {
        public CreatureLua Creature = new();
        public EffectLua Effect = new();
        public BackpackLua Backpack = new();
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
        public void Log(string msg)
        {
            Debug.Log(msg);
        }
        public Vector3 RotateVec(Vector3 vec, float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward) * vec;
        }
    }
}