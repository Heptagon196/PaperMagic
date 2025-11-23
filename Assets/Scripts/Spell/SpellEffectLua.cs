using PMLua;
using UnityEngine;
using XLua;

namespace Spell
{
    [CSharpCallLua]
    public delegate void LuaCallOnUpdate(LuaTable self, float deltaTime);
    [CSharpCallLua]
    public delegate void LuaCallOnApply(LuaTable self, Vector3 spawnPos, Vector3 spawnTowards);
    [CSharpCallLua]
    public delegate void LuaCallOnExpired(LuaTable self);
    [CSharpCallLua]
    public delegate void LuaCallTriggerEvent(LuaTable self, Collider other);
    [CSharpCallLua]
    public delegate void LuaCallCollisionEvent(LuaTable self, Collision other);
    [LuaCallCSharp]
    public class SpellEffectLua : SpellEffectBase
    {
        public string LuaScriptPath;
        private string _id;
        private LuaScriptExecutor _script;
        private LuaTable _module;
        private LuaCallOnUpdate _update;
        private LuaCallOnExpired _onExpired;
        private LuaCallOnApply _applyEffect;
        private LuaCallCollisionEvent _collisionEnter;
        private LuaCallCollisionEvent _collisionStay;
        private LuaCallCollisionEvent _collisionExit;
        private LuaCallTriggerEvent _triggerEnter;
        private LuaCallTriggerEvent _triggerStay;
        private LuaCallTriggerEvent _triggerExit;
        private bool _toDispose = false;
        public override SpellEffectBase SpawnEffectByPath(string path)
        {
            var ret = new SpellEffectLua
            {
                LuaScriptPath = path
            };
            ret.OnInit();
            return ret;
        }
        public override void SetFloat(string key, float value)
        {
            _module.Set(key, value);
        }
        public override float GetFloat(string key)
        {
            return _module.Get<float>(key);
        }
        public override void SetString(string key, string value)
        {
            _module.Set(key, value);
        }
        public override string GetString(string key)
        {
            return _module.Get<string>(key);
        }
        public override void SetObject(string key, object value)
        {
            _module.Set(key, value);
        }
        public override object GetObject(string key)
        {
            return _module.Get<object>(key);
        }
        public override bool ContainsData(string key)
        {
            return _module.ContainsKey(key);
        }
        public override string GetID()
        {
            return _id;
        }
        public override void OnInit()
        {
            _script = new LuaScriptExecutor
            {
                luaScriptPath = LuaScriptPath
            };
            _script.InitScriptEnv();
            _module = _script.RunScript();
            if (_module == null)
            {
                return;
            }
            _id = _module.Get<string>("ID");
            _update = _module.Get<LuaCallOnUpdate>("OnUpdate");
            _applyEffect = _module.Get<LuaCallOnApply>("OnApply");
            _onExpired = _module.Get<LuaCallOnExpired>("OnExpired");
            _collisionEnter = _module.Get<LuaCallCollisionEvent>("OnCollisionEnter");
            _collisionStay = _module.Get<LuaCallCollisionEvent>("OnCollisionStay");
            _collisionExit = _module.Get<LuaCallCollisionEvent>("OnCollisionExit");
            _triggerEnter = _module.Get<LuaCallTriggerEvent>("OnTriggerEnter");
            _triggerStay = _module.Get<LuaCallTriggerEvent>("OnTriggerStay");
            _triggerExit = _module.Get<LuaCallTriggerEvent>("OnTriggerExit");
            Inited = true;
        }
        public override void ApplyEffect(Vector3 spawnLocation, Vector3 spawnTowards)
        {
            if (Inited)
            {
                _applyEffect?.Invoke(_module, spawnLocation, spawnTowards);
                Applied = true;
            }
            base.ApplyEffect(spawnLocation, spawnTowards);
        }
        public override void TriggerOnUpdate(float deltaTime)
        {
            if (Inited && Applied)
            {
                _update?.Invoke(_module, deltaTime);
            }
            base.TriggerOnUpdate(deltaTime);
        }
        public override void TriggerOnTriggerEnter(Collider other)
        {
            if (!Applied || other.gameObject == null)
            {
                return;
            }
            _triggerEnter?.Invoke(_module, other);
            base.TriggerOnTriggerEnter(other);
        }

        public override void TriggerOnTriggerStay(Collider other)
        {
            if (!Applied || other.gameObject == null)
            {
                return;
            }
            _triggerStay?.Invoke(_module, other);
            base.TriggerOnTriggerStay(other);
        }

        public override void TriggerOnTriggerExit(Collider other)
        {
            if (!Applied || other.gameObject == null)
            {
                return;
            }
            _triggerExit?.Invoke(_module, other);
            base.TriggerOnTriggerExit(other);
        }

        public override void TriggerOnCollisionEnter(Collision other)
        {
            if (!Applied || other.gameObject == null)
            {
                return;
            }
            _collisionEnter?.Invoke(_module, other);
            base.TriggerOnCollisionEnter(other);
        }

        public override void TriggerOnCollisionStay(Collision other)
        {
            if (!Applied || other.gameObject == null)
            {
                return;
            }
            _collisionStay?.Invoke(_module, other);
            base.TriggerOnCollisionStay(other);
        }

        public override void TriggerOnCollisionExit(Collision other)
        {
            if (!Applied || other.gameObject == null)
            {
                return;
            }
            _collisionExit?.Invoke(_module, other);
            base.TriggerOnCollisionExit(other);
        }
        
        public override void TriggerOnExpired()
        {
            if (!Applied)
            {
                return;
            }
            Applied = false;
            _onExpired?.Invoke(_module);
            base.TriggerOnExpired();
            LuaManager.Instance.ToDestroyScript(_script);
            _script = null;
        }
        
        ~SpellEffectLua()
        {
            if (!Applied)
            {
                return;
            }
            Applied = false;
            LuaManager.Instance.ToDestroyScript(_script);
            _script = null;
        }
    }
}