using NPC;
using UnityEngine;
using XLua;

namespace Spell
{
    public class Projectile : MonoBehaviour
    {
        public CreatureBase source;
        private SpellEffectBase _usingEffect;
        private float _callPerSecond = 0.1f;
        private float _lastCallTime = 0f;
        private float _expireTime = 0f;
        private float _spawnTimeStamp = 0f;
        public SpellEffectBase GetEffect()
        {
            return _usingEffect;
        }
        public void Spawn(Vector3 spawnLocation, Vector3 spawnTowards, SpellEffectBase effect)
        {
            _usingEffect = effect;
            source = effect.Source;
            effect.Owner = gameObject;
            effect.SetObject("Owner", gameObject);
            effect.SetObject("Source", source);
            _spawnTimeStamp = Time.time;
            transform.position = spawnLocation;
            if (effect.UsePlayerSpawnInfo)
            {
                effect.SpawnPosition = spawnLocation;
                effect.SpawnTowards = spawnTowards.normalized;
            }
            effect?.ApplyEffect(effect.SpawnPosition, effect.SpawnTowards);
            _callPerSecond = effect?.GetFloat("UpdateInterval") ?? -1f;
            _expireTime = effect?.GetFloat("ExpireTime") ?? -1f;
        }
        public void Update()
        {
            if (_expireTime >= 0 && Time.time - _spawnTimeStamp > _expireTime)
            {
                DestroyProjectile();
                return;
            }
            if (_callPerSecond == 0)
            {
                _usingEffect?.TriggerOnUpdate(Time.fixedDeltaTime);
            }
            else if (_callPerSecond > 0 && Time.time - _lastCallTime > _callPerSecond)
            {
                _usingEffect?.TriggerOnUpdate(Time.fixedDeltaTime);
                _lastCallTime = Time.time;
            }
        }
        public void DestroyProjectile()
        {
            _usingEffect?.TriggerOnExpired();
            gameObject.SetActive(false);
            _usingEffect = null;
            _expireTime = 0;
        }
        public void OnTriggerEnter(Collider other)
        {
            _usingEffect?.TriggerOnTriggerEnter(other);
        }
        public void OnTriggerStay(Collider other)
        {
            _usingEffect?.TriggerOnTriggerStay(other);
        }
        public void OnTriggerExit(Collider other)
        {
            _usingEffect?.TriggerOnTriggerExit(other);
        }
        public void OnCollisionEnter(Collision other)
        {
            _usingEffect?.TriggerOnCollisionEnter(other);
        }
        public void OnCollisionStay(Collision other)
        {
            _usingEffect?.TriggerOnCollisionStay(other);
        }
        public void OnCollisionExit(Collision other)
        {
            _usingEffect?.TriggerOnCollisionExit(other);
        }
    }
}