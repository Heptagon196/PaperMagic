using System;
using System.Collections.Generic;
using Controller;
using PMLua;
using Quest;
using UnityEngine;
using XLua;

namespace NPC
{
    [Serializable, LuaCallCSharp]
    public enum CreatureLevel
    {
        None = -1,
        Normal = 0,
        Elite = 1,
        Boss = 2,
    }
    [Serializable, LuaCallCSharp]
    public abstract class CreatureInfoBase
    {
        public string id;
        public string name;
        public CreatureFaction faction;
        public CreatureLevel level;
        public float maxHealth;
        public string path;
        public float width;
        public float height;
        public int xid;
        public Dictionary<string, List<string>> AnimationSprites = new();
        public Dictionary<string, float> AnimationSpriteSwitchDurations = new();
        public abstract CreatureInfoBase Clone();
        public abstract void OnUpdate();
        public abstract void OnStart(GameObject owner);
        public abstract void OnDeath();
        public abstract void SetIsOnGround(bool isOnGround);
    }
    [CSharpCallLua]
    public interface ICreatureAnimLua
    {
        // ReSharper disable once InconsistentNaming
        public string anim { get; }
        // ReSharper disable once InconsistentNaming
        public int num { get; }
        // ReSharper disable once InconsistentNaming
        public float duration { get; }
    }
    [CSharpCallLua]
    public delegate void OnCreatureAction(LuaTable self);
    public class CreatureInfoLua : CreatureInfoBase
    {
        private readonly LuaTable _module;
        private readonly OnCreatureAction _onUpdate;
        private readonly OnCreatureAction _onStart;
        private readonly OnCreatureAction _onDeath;
        public CreatureInfoLua(LuaTable table, string scriptPath)
        {
            if (table == null)
            {
                return;
            }
            _module = table;
            path = scriptPath;
            id = table.Get<string>("ID");
            name = table.Get<string>("Name");
            faction = (CreatureFaction)table.Get<int>("Faction");
            level = (CreatureLevel)table.Get<int>("Level");
            maxHealth = table.Get<float>("Health");
            width = table.TryGet<float>("Width", 1);
            height = table.TryGet<float>("Height", 2);
            var spriteFolder = table.Get<string>("AnimationFolder");
            var spriteSet = table.Get<List<ICreatureAnimLua>>("Animations");
            _onUpdate = table.Get<OnCreatureAction>("OnUpdate");
            _onStart = table.Get<OnCreatureAction>("OnStart");
            _onDeath = table.Get<OnCreatureAction>("OnDeath");
            xid = CreatureBase.AllocCreatureXid();
            table.Set("XID", xid);
            AnimationSprites.Clear();
            AnimationSpriteSwitchDurations.Clear();
            foreach (var sprite in spriteSet)
            {
                var animPrefix = sprite.anim;
                AnimationSprites[animPrefix] = new();
                AnimationSpriteSwitchDurations.Add(animPrefix, sprite.duration);
                for (var idx = 1; idx <= sprite.num; idx++)
                {
                    var spritePath = $"{spriteFolder}/{animPrefix}_{idx}.png";
                    AnimationSprites[animPrefix].Add(spritePath);
                }
            }
        }
        public override CreatureInfoBase Clone()
        {
            var script = new LuaScriptExecutor
            {
                luaScriptPath = path
            };
            script.InitScriptEnv();
            return new CreatureInfoLua(script.RunScript(false), path);
        }
        public override void OnUpdate()
        {
            _onUpdate?.Invoke(_module);
        }
        public override void OnStart(GameObject owner)
        {
            _module.Set("Owner", owner);
            _onStart?.Invoke(_module);
        }
        public override void OnDeath()
        {
            _onDeath?.Invoke(_module);
        }
        public override void SetIsOnGround(bool isOnGround)
        {
            _module.Set("isGrounded", isOnGround);
        }
    }
    public class CreatureBehaviour :  CreatureBase
    {
        public string creatureID;
        private CreatureAnimation _creatureAnimation;
        private CreatureMovement _creatureMovement;
        public CreatureInfoBase CreatureInfo;
        public bool creatureInited = false;

        private void Awake()
        {
            healthPoint = 1;
            maxHealthPoint = 1;
            _creatureAnimation = GetComponent<CreatureAnimation>();
            _creatureMovement = GetComponent<CreatureMovement>();
        }
        private void Start()
        {
            if (creatureID != "")
            {
                InitCreature(CreatureManager.SpawnCreatureInfoByID(creatureID));
            }
        }
        public void SetToDead()
        {
            if (!creatureInited)
            {
                return;
            }
            CreatureInfo?.OnDeath();
            creatureInited = false;
            Destroy(gameObject);
            new GameObject().AddComponent<CreatureDeathAnimation>().InitAnim(_creatureAnimation);
        }
        public void InitCreature(CreatureInfoBase info)
        {
            CreatureInfo = info;
            healthPoint = info.maxHealth;
            faction = info.faction;
            maxHealthPoint = healthPoint;
            creatureInited = true;
            _creatureAnimation.LoadSpriteSet(info.AnimationSprites, info.AnimationSpriteSwitchDurations);
            _creatureMovement.SetCreature(info);

            var capsuleCollider = transform.GetComponent<CapsuleCollider>();
            if (capsuleCollider != null)
            {
                capsuleCollider.radius = info.width / 2;
                capsuleCollider.height = info.height;
            }
        }
        private void Update()
        {
            if (creatureInited && healthPoint <= 0)
            {
                SetToDead();
            }
        }
        private bool _onDeathCalled = false;
        public override void OnTakeDamage(float damage, CreatureBase source)
        {
            if (healthPoint == 0)
            {
                if (!_onDeathCalled)
                {
                    _onDeathCalled = true;
                    EventManager.Broadcast(QuestNotifyEvent.EnemyKill, gameObject, creatureID);
                }
            }
        }
    }
}