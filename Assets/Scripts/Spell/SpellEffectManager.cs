using System.Collections.Generic;
using System.IO;
using PMLua;
using UnityEngine;

namespace Spell
{
    public static class SpellEffectManager
    {
        private struct SpellEffectSpawnInfo
        {
            public SpellEffectBase BaseEffect;
            public string Path;
        }
        private static readonly Dictionary<string, SpellEffectSpawnInfo> AllSpellEffectsSpawner = new();
        public static SpellEffectBase SpawnEffect(string name)
        {
            AllSpellEffectsSpawner.TryGetValue(name, out SpellEffectSpawnInfo spawnInfo);
            if (spawnInfo.BaseEffect == null)
            {
                Debug.LogError("SpellEffect not found: " + name);
                return null;
            }
            return spawnInfo.BaseEffect.SpawnEffectByPath(spawnInfo.Path);
        }
        public static void RegisterAllSpellEffects()
        {
            RegisterAllLuaSpellEffects();
        }
        private static void RegisterAllLuaSpellEffects()
        {
            SpellEffectLua baseEffectLua = new SpellEffectLua();
            LuaManager.RegisterAllLuaScriptsOf("SpellEffects", (id, path, module) =>
            {
                AllSpellEffectsSpawner.Add(id,
                    new SpellEffectSpawnInfo() {
                        BaseEffect = baseEffectLua,
                        Path = path,
                    }
                );
            });
        }
    }
}