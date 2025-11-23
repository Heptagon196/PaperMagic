using NPC;
using Spell;
using UnityEngine;
using XLua;

namespace PMLua.Export
{
    [LuaCallCSharp]
    public class ProjectileLua
    {
        public void Destroy(GameObject projectile)
        {
            projectile.GetComponent<Projectile>()?.DestroyProjectile();
        }
        public SpellEffectBase GetProjectileEffect(GameObject projectile)
        {
            return projectile.GetComponent<Projectile>()?.GetEffect();
        }
        public bool TryDoDamage(GameObject projectile, GameObject target, float damage)
        {
            var targetCreature = target.GetComponent<CreatureBase>();
            if (targetCreature != null)
            {
                var sourceCreature = projectile.GetComponent<Projectile>()?.source;
                if (sourceCreature == targetCreature)
                {
                    return false;
                }
                targetCreature.TakeDamage(damage, sourceCreature);
                return true;
            }
            return false;
        }
    }
}