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
    public enum CreatureFaction
    {
        Friendly, // 友好
        Hostile, // 敌对
        Neutral, // 中立
    }
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
        public Dictionary<CreatureAnimationStage, List<string>> AnimationSprites = new();
        public Dictionary<CreatureAnimationStage, float> AnimationSpriteSwitchDurations = new();
        public abstract CreatureInfoBase Clone();
        public abstract void OnUpdate();
        public abstract void OnStart(GameObject owner);
        public abstract void SetIsOnGround(bool isOnGround);
    }
    [CSharpCallLua]
    public interface ICreatureAnimLua
    {
        // ReSharper disable once InconsistentNaming
        public int anim { get; }
        // ReSharper disable once InconsistentNaming
        public int num { get; }
        // ReSharper disable once InconsistentNaming
        public float duration { get; }
    }
    [CSharpCallLua]
    public delegate void OnCreatureAction(LuaTable self);
    public class CreatureInfoLua : CreatureInfoBase
    {
        public static readonly Dictionary<CreatureAnimationStage, string> SpriteGroupName = new()
        {
            { CreatureAnimationStage.Death, "death" },
            { CreatureAnimationStage.Idle, "idle" },
            { CreatureAnimationStage.Walk, "walk" },
            { CreatureAnimationStage.Run, "run" },
            { CreatureAnimationStage.Jump, "jump" },
            { CreatureAnimationStage.Attack, "attack" },
            { CreatureAnimationStage.Shield, "shield" },
        };
        private readonly LuaTable _module;
        private readonly OnCreatureAction _onUpdate;
        private readonly OnCreatureAction _onStart;
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
            var spriteFolder = table.Get<string>("AnimationFolder");
            var spriteSet = table.Get<List<ICreatureAnimLua>>("Animations");
            _onUpdate = table.Get<OnCreatureAction>("OnUpdate");
            _onStart = table.Get<OnCreatureAction>("OnStart");
            AnimationSprites.Clear();
            AnimationSpriteSwitchDurations.Clear();
            foreach (var sprite in spriteSet)
            {
                var stat = (CreatureAnimationStage)sprite.anim;
                var spritePrefix = SpriteGroupName.GetValueOrDefault(stat);
                AnimationSprites[stat] = new();
                AnimationSpriteSwitchDurations.Add(stat, sprite.duration);
                for (var idx = 1; idx <= sprite.num; idx++)
                {
                    var spritePath = $"{spriteFolder}/{spritePrefix}_{idx}.png";
                    AnimationSprites[stat].Add(spritePath);
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
            creatureInited = false;
            Destroy(gameObject);
            new GameObject().AddComponent<CreatureDeathAnimation>().InitAnim(_creatureAnimation);
        }
        public void InitCreature(CreatureInfoBase info)
        {
            CreatureInfo = info;
            healthPoint = info.maxHealth;
            maxHealthPoint = healthPoint;
            creatureInited = true;
            _creatureAnimation.LoadSpriteSet(info.AnimationSprites, info.AnimationSpriteSwitchDurations);
            _creatureMovement.SetCreature(info);
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