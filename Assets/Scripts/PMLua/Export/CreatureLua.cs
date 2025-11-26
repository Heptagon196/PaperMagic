using System.Collections.Generic;
using Backpack;
using NPC;
using Spell;
using UnityEngine;
using XLua;

namespace PMLua.Export
{
    [LuaCallCSharp]
    public class CreatureLua
    {
        public int GetCreatureLevel(GameObject obj)
        {
            return (int)(obj.GetComponent<CreatureBehaviour>()?.CreatureInfo?.level ?? CreatureLevel.None);
        }
        public int GetCreatureFaction(GameObject obj)
        {
            return (int)(obj.GetComponent<CreatureBehaviour>()?.CreatureInfo?.faction ?? CreatureFaction.Friendly);
        }
        public CreatureInfoBase GetCreatureInfo(GameObject obj)
        {
            return obj.GetComponent<CreatureBehaviour>()?.CreatureInfo;
        }
        public bool DoDamage(GameObject source, GameObject target, float damage)
        {
            var targetCreature = target.GetComponent<CreatureBase>();
            if (targetCreature != null)
            {
                targetCreature.TakeDamage(damage, source?.GetComponent<CreatureBase>());
                return true;
            }
            return false;
        }
        public GameObject Spawn(string id, Vector3 spawnPosition)
        {
            return CreatureManager.SpawnCreature(id, spawnPosition);
        }
        public void SetPersistent(GameObject creature)
        {
            CreatureManager.SetCreaturePersistent(creature);
        }
        public List<SpellEffectBase> CastSpell(GameObject source, string spellID, Vector3 spawnPosition, Vector3 spawnTowards)
        {
            var spell = SpellManager.SpawnSpell(spellID);
            spell.OnInit();
            spell.Execute(out var cost ,out var effectList);
            foreach (var effect in effectList)
            {
                effect.UsePlayerSpawnInfo = false;
                effect.SpawnPosition = spawnPosition;
                effect.SpawnTowards = spawnTowards.normalized;
                effect.Source = source.GetComponent<CreatureBase>();
                    
                var bullet = ProjectilePool.GetObject();
                bullet.GetComponent<Projectile>().Spawn(effect.SpawnPosition, effect.SpawnTowards, effect);
            }
            return effectList;
        }
        public void MoveTowards(GameObject gameObject, Vector3 target, float speed)
        {
            gameObject.transform.position =
                Vector3.MoveTowards(gameObject.transform.position, target, speed * Time.deltaTime);
        }
        public bool CanSeeTarget(GameObject owner, GameObject target, float maxDistance = 10f)
        {
            Vector3 direction = target.transform.position - owner.transform.position;
            if (direction.magnitude > maxDistance)
            {
                return false;
            }
            if (Physics.Raycast(owner.transform.position, direction.normalized, out var hit, maxDistance,
                    LayerMask.GetMask("Player", "Ground")))
            {
                return hit.collider.gameObject == target;
            }
            return false;
        }
        public void SetAnimation(GameObject owner, string stage)
        {
            var anim = owner.GetComponent<CreatureAnimation>();
            anim.SetAnimStat(stage);
        }
        public List<SpellEffectBase> CastSpellTree(GameObject source, List<List<string>> spellID, Vector3 spawnPosition, Vector3 spawnTowards)
        {
            var scheme = new SpellTreeSchemeData();
            foreach (var spellList in spellID)
            {
                scheme.schemeData.Add(new SpellTreeSchemeColumnData()
                {
                    columnData = spellList
                });
            }
            var spell = scheme.CreateSpellTree();
            spell.OnInit();
            spell.Execute(out var cost ,out var effectList);
            foreach (var effect in effectList)
            {
                effect.UsePlayerSpawnInfo = false;
                effect.SpawnPosition = spawnPosition;
                effect.SpawnTowards = spawnTowards.normalized;
                effect.Source = source.GetComponent<CreatureBase>();
                    
                var bullet = ProjectilePool.GetObject();
                bullet.GetComponent<Projectile>().Spawn(effect.SpawnPosition, effect.SpawnTowards, effect);
            }
            return effectList;
        }
    }
}