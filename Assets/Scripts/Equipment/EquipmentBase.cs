using System;
using System.Collections.Generic;
using Controller;
using NPC;
using Spell;
using UI.General;
using UnityEngine;

namespace Equipment
{
    public enum EquipmentCastType
    {
        Automatic, // 自动定期释放
        Passive, // 被动触发
        PressKey, // 按键触发
    }
    [Serializable, Flags]
    public enum EquipmentSlot
    {
        Hat = 1 << 1, // 头部
        Coat = 1 << 2, // 衣服
        WeaponLeft = 1 << 3, // 左手武器
        WeaponRight = 1 << 4, // 右手武器
        Foot = 1 << 5, // 鞋子
        
        Weapon = WeaponLeft | WeaponRight,
        All = Hat | Coat | WeaponLeft | WeaponRight | Foot,
        None = 0,
    }
    public enum ExecuteSpellResult
    {
        Success,
        InCoolDown,
        ManaNotEnough,
    }
    [Serializable]
    public abstract class EquipmentBase
    {
        // 装备标识符
        public string equipmentID;
        // 名称
        public string equipmentName;
        // 描述
        public string equipmentDesc;
        // 图标
        public string equipmentIcon;
        // 容纳法术数量
        public int equipmentCapacity;
        // 施法类型
        public EquipmentCastType castType;
        // 装备槽位
        public EquipmentSlot slot;
        // MP上限
        public float maxMana = 100;
        // MP恢复速度
        public float manaResumePerSecond = 10;
        // 施法间隔
        public float minCastInterval = 1f;

        // 装备法术
        public SpellTreeBase SpellPanel = new SpellTreeBaseVirtualRoot();

        public float currentMana = 0;
        public float ManaPercent => currentMana / maxMana;
        private float _lastCastTimeStamp = 0;
        // private const float ResumeManaInterval = 0.1f;
        // private float _lastResumeTimeStamp = 0;
        private PlayerController _player;

        public virtual void OnAdd() {}
        public virtual void OnDel() {}

        public EquipmentInstance CreateEquipmentInstance()
        {
            return new EquipmentInstance
            {
                equipmentID = equipmentID,
                equipmentName = equipmentName,
                equipmentDesc = equipmentDesc,
                equipmentIcon = equipmentIcon,
                equipmentCapacity = equipmentCapacity,
                slot = slot,
                castType = castType,
                maxMana = maxMana,
                manaResumePerSecond = manaResumePerSecond,
                minCastInterval = minCastInterval,
            };
        }

        public void InitWeapon(PlayerController player)
        {
            currentMana = maxMana;
            _player = player;
            OnAdd();
        }

        ~EquipmentBase()
        {
            OnDel();
        }

        public void ResumeManaOnUpdate()
        {
            // 鞋部只在地面上回复
            if (slot == EquipmentSlot.Foot && !_player.IsGrounded)
            {
                return;
            }
            // if (Time.time - _lastResumeTimeStamp > ResumeManaInterval)
            // {
            // _lastResumeTimeStamp = Time.time;
            currentMana += manaResumePerSecond * Time.deltaTime;
            currentMana = Math.Min(currentMana, maxMana);
            // }
        }
        public void InitSpellTree(SpellTreeBase treeBase)
        {
            SpellPanel = treeBase;
            SpellPanel.OnInit();
        }
        public ExecuteSpellResult ExecuteSpell(CreatureBase source)
        {
            if (Time.time - _lastCastTimeStamp < minCastInterval)
            {
                return ExecuteSpellResult.InCoolDown;
            }
            List<SpellEffectBase> effects = null;
            int cost = 0;
            SpellPanel?.Execute(out cost, out effects);
            if (cost > currentMana + Mathf.Epsilon)
            {
                return ExecuteSpellResult.ManaNotEnough;
            }
            if (effects != null)
            {
                currentMana -= cost;
                foreach (var effect in effects)
                {
                    var bullet = ProjectilePool.GetObject();
                    effect.Source = source;
                    bullet.GetComponent<Projectile>().Spawn(_player.GetCastLocation(), _player.GetCastTowards(), effect);
                }
            }
            _lastCastTimeStamp = Time.time;
            return ExecuteSpellResult.Success;
        }
    }
    public class EquipmentInstance : EquipmentBase {}
    public class EquipmentEmpty : EquipmentBase
    {
        public EquipmentEmpty()
        {
            equipmentID = EquipmentManager.EmptyEquipment;
            equipmentName = EquipmentManager.EmptyEquipment;
            slot = EquipmentSlot.All;
            castType = EquipmentCastType.Passive;
            equipmentIcon = "add_sign.png";
        }
    }
}
