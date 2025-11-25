using System.Collections.Generic;
using NPC;
using UnityEngine;
using XLua;

namespace Spell
{
    [CSharpCallLua]
    public delegate void PatchOnTriggerEvent(SpellEffectBase selfEffect, Collider other);
    [CSharpCallLua]
    public delegate void PatchOnCollisionEvent(SpellEffectBase selfEffect, Collision other);
    [CSharpCallLua]
    public delegate void PatchOnApplyEvent(SpellEffectBase selfEffect, Vector3 spawnLocation, Vector3 spawnTowards);
    [CSharpCallLua]
    public delegate void PatchOnUpdateEvent(SpellEffectBase selfEffect, float deltaTime);
    [CSharpCallLua]
    public delegate void PatchOnExpiredEvent(SpellEffectBase selfEffect);
    [LuaCallCSharp]
    public abstract class SpellEffectBase
    {
        public bool Inited = false;
        public bool Applied = false;
        public bool UsePlayerSpawnInfo = true;
        public CreatureBase Source;
        public GameObject Owner;
        public Vector3 SpawnPosition = Vector3.zero;
        public Vector3 SpawnTowards = Vector3.right;
        public readonly Dictionary<SpellTreeBase, int> BranchPosition = new();
        public abstract SpellEffectBase SpawnEffectByPath(string path);
        public abstract void SetFloat(string key, float value);
        public abstract float GetFloat(string key);
        public abstract void SetString(string key, string value);
        public abstract string GetString(string key);
        public abstract void SetObject(string key, object value);
        public abstract object GetObject(string key);
        public abstract bool ContainsData(string key);
        public abstract string GetID();

        public abstract void OnInit();
        public List<PatchOnApplyEvent> OnApply = new();
        public virtual void ApplyEffect(Vector3 spawnLocation, Vector3 spawnTowards)
        {
            spawnTowards = new Vector3(spawnTowards.x, spawnTowards.y, 0).normalized;
            OnApply.ForEach(patch => patch(this, spawnLocation, spawnTowards));
        }
        public List<PatchOnUpdateEvent> OnUpdate = new();
        public virtual void TriggerOnUpdate(float deltaTime)
        {
            OnUpdate.ForEach(patch => patch(this, deltaTime));
        }
        public List<PatchOnTriggerEvent> OnTriggerEnter = new();
        public virtual void TriggerOnTriggerEnter(Collider other)
        {
            OnTriggerEnter.ForEach(patch => patch(this, other));
        }
        public List<PatchOnTriggerEvent> OnTriggerStay = new();
        public virtual void TriggerOnTriggerStay(Collider other)
        {
            OnTriggerStay.ForEach(patch => patch(this, other));
        }
        public List<PatchOnTriggerEvent> OnTriggerExit = new();
        public virtual void TriggerOnTriggerExit(Collider other)
        {
            OnTriggerExit.ForEach(patch => patch(this, other));
        }
        public List<PatchOnCollisionEvent> OnCollisionEnter = new();
        public virtual void TriggerOnCollisionEnter(Collision other)
        {
            OnCollisionEnter.ForEach(patch => patch(this, other));
        }
        public List<PatchOnCollisionEvent> OnCollisionStay = new();
        public virtual void TriggerOnCollisionStay(Collision other)
        {
            OnCollisionStay.ForEach(patch => patch(this, other));
        }
        public List<PatchOnCollisionEvent> OnCollisionExit = new();
        public virtual void TriggerOnCollisionExit(Collision other)
        {
            OnCollisionExit.ForEach(patch => patch(this, other));
        }
        public List<PatchOnExpiredEvent> OnExpired = new();
        public virtual void TriggerOnExpired()
        {
            OnExpired.ForEach(patch => patch(this));
        }
    }
}