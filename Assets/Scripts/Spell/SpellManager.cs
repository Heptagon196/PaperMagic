using System.Collections.Generic;
using System.IO;
using PMLua;
using UnityEngine;

namespace Spell
{
    public static class SpellManager
    {
        private struct SpellSpawnInfo
        {
            public SpellTreeBase BaseSpell;
            public string Path;
        }
        public const string EmptySpell = "std.empty";
        private static readonly Dictionary<string, SpellSpawnInfo> AllSpellsSpawner = new();
        public static SpellTreeBase SpawnSpell(string name)
        {
            AllSpellsSpawner.TryGetValue(name, out SpellSpawnInfo spawnInfo);
            if (spawnInfo.BaseSpell == null)
            {
                Debug.LogError("Spell not found: " + name);
                return null;
            }
            return spawnInfo.BaseSpell.SpawnSpellByPath(spawnInfo.Path);
        }
        public static void RegisterAllSpells()
        {
            RegisterAllLuaSpells();
            AllSpellsSpawner.Add(EmptySpell, new SpellSpawnInfo()
            {
                BaseSpell = new SpellTreeBaseEmpty()
            });
        }
        private static void RegisterAllLuaSpells()
        {
            SpellTreeBaseLua spellBaseLua = new SpellTreeBaseLua();
            LuaManager.RegisterAllLuaScriptsOf("Spells", (id, path, module) =>
            {
                AllSpellsSpawner.Add(id,
                    new SpellSpawnInfo() {
                        BaseSpell = spellBaseLua,
                        Path = path,
                    }
                );
            });
        }
    }
}