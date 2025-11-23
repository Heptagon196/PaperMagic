using NPC;
using Spell;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using XLua;

namespace PMLua.Export
{
    [LuaCallCSharp]
    public class EffectLua
    {
        public SpellEffectBase Spawn(string effectName, Vector3 spawnPosition, Vector3 spawnTowards)
        {
            var effect = SpellEffectManager.SpawnEffect(effectName);
            effect.UsePlayerSpawnInfo = false;
            effect.SpawnPosition = spawnPosition;
            effect.SpawnTowards = spawnTowards.normalized;
            return effect;
        }
        public SpellEffectBase Spawn(string effectName)
        {
            var effect = SpellEffectManager.SpawnEffect(effectName);
            effect.UsePlayerSpawnInfo = true;
            return effect;
        }
        public GameObject Instantiate(SpellEffectBase effect, Vector3 spawnPosition, Vector3 spawnTowards, CreatureBase source)
        {
            var bullet = ProjectilePool.GetObject();
            effect.Source = source;
            bullet.GetComponent<Projectile>().Spawn(spawnPosition, spawnTowards, effect);
            return bullet;
        }
    }
}