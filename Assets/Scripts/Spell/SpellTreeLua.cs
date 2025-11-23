using System;
using System.Collections.Generic;
using PMLua;
using UnityEngine;
using XLua;

namespace Spell
{
    [CSharpCallLua]
    public delegate int LuaCallOnCast(LuaTable self, List<SpellEffectBase> effectList, SpellTreeBase spellTree);
    [Serializable, LuaCallCSharp]
    public class SpellTreeBaseLua : SpellTreeBase
    {
        public string luaScriptPath;
        [HideInInspector] public LuaScriptExecutor script;
        public LuaCallOnCast OnCastSpell;
        private LuaTable _module;
        public override void OnInit()
        {
            script = new LuaScriptExecutor
            {
                luaScriptPath = luaScriptPath
            };
            script.InitScriptEnv();
            _module = script.RunScript();
            if (_module == null)
            {
                return;
            }
            OnCastSpell = _module.Get<LuaCallOnCast>("OnCast");
            maxChildNodeCount = _module.TryGet<int>("ChildNode", 0);
            spellType = (SpellType)(_module.TryGet<int>("Category", 0));
            spellName = _module.Get<string>("Name") ?? "";
            spellDesc = _module.Get<string>("Desc") ?? "";
            id = _module.Get<string>("ID") ?? "";
            iconPath = _module.Get<string>("Icon") ?? "";
            spellCost = _module.TryGet<float>("Cost", 0);
            spellDamage = _module.TryGet<float>("Damage", 0);
            base.OnInit();
        }
        public override void Execute(out int cost, out List<SpellEffectBase> effectList)
        {
            cost = 0;
            effectList = new List<SpellEffectBase>();
            for (int idx = 0; idx < maxChildNodeCount && idx < Nodes.Count; idx++)
            {
                Nodes[idx].Execute(out var subCost, out var childEffectList);
                foreach (var childEffect in childEffectList)
                {
                    childEffect.BranchPosition.Add(this, idx);
                }
                cost += subCost;
                effectList.AddRange(childEffectList);
            }
            if (_module != null && OnCastSpell != null)
            {
                cost += OnCastSpell.Invoke(_module, effectList, this);
            }
        }
        public override SpellTreeBase SpawnSpellByPath(string path)
        {
            var ret = new SpellTreeBaseLua
            {
                luaScriptPath = path
            };
            ret.OnInit();
            return ret;
        }
        public override int GetMaxNodeCount()
        {
            return maxChildNodeCount;
        }
        ~SpellTreeBaseLua()
        {
            LuaManager.Instance.ToDestroyScript(script);
            script = null;
        }
    }
}