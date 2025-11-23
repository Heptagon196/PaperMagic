using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Spell
{
    public enum SpellType
    {
        Projectile,
        Modifier,
        Other,
    }
    // 法术构筑
    [Serializable, LuaCallCSharp]
    public abstract class SpellTreeBase
    {
        public string id;
        public int maxChildNodeCount = 1;
        public float spellCost = 0;
        public float spellDamage = 0;
        public SpellType spellType = SpellType.Projectile;
        public string spellName;
        public string spellDesc;
        public string iconPath;
        public readonly List<SpellTreeBase> Nodes = new();
        [HideInInspector] public SpellTreeBase Parent;
        public int GetEffectInSpellTreePos(SpellEffectBase effect)
        {
            effect.BranchPosition.TryGetValue(this, out var ret);
            return ret;
        }
        public int ChildNodeCount()
        {
            return Nodes.Count;
        }
        public virtual void OnInit()
        {
            foreach (var node in Nodes)
            {
                node.Parent = this;
                node.OnInit();
            }
        }
        public abstract void Execute(out int cost, out List<SpellEffectBase> effectList);
        public virtual int GetSize()
        {
            int num = 1;
            foreach (var node in Nodes)
            {
                num += node.GetSize();
            }
            return num;
        }
        public abstract SpellTreeBase SpawnSpellByPath(string path);
        public abstract int GetMaxNodeCount();
    }

    public class SpellTreeBaseEmpty : SpellTreeBase
    {
        public override void OnInit()
        {
            id = SpellManager.EmptySpell;
            maxChildNodeCount = 0;
            spellCost = 0;
            spellDamage = 0;
            spellType = SpellType.Projectile;
            spellName = "empty";
            spellDesc = "empty";
            iconPath = "add_sign.png";
            base.OnInit();
        }
        public override void Execute(out int cost, out List<SpellEffectBase> effectList)
        {
            cost = 0;
            effectList = new();
        }
        public override int GetSize() => 0;
        public override SpellTreeBase SpawnSpellByPath(string path)
        {
            var ret = new SpellTreeBaseEmpty();
            ret.OnInit();
            return ret;
        }
        public override int GetMaxNodeCount()
        {
            return 0;
        }
    }
    
    public class SpellTreeBaseVirtualRoot : SpellTreeBase
    {
        public override void Execute(out int cost, out List<SpellEffectBase> effectList)
        {
            cost = 0;
            effectList = new List<SpellEffectBase>();
            if (Nodes.Count > 0)
            {
                Nodes[0].Parent = this;
                Nodes[0].Execute(out var subCost, out var subNodeEffects);
                cost += subCost;
                effectList.AddRange(subNodeEffects);
            }
        }
        public override SpellTreeBase SpawnSpellByPath(string path)
        {
            return new SpellTreeBaseVirtualRoot();
        }
        public override int GetMaxNodeCount()
        {
            return 1;
        }
    }
}