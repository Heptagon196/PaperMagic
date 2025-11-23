using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PMLua;
using Spell;
using UnityEngine;

namespace Equipment
{
    public static class EquipmentManager
    {
        public const string EmptyEquipment = "std.empty";
        private static readonly Dictionary<string, EquipmentBase> AllEquipmentSpawner = new();
        public static readonly List<EquipmentSlot> AvailableEquipmentSlots = new()
        {
            EquipmentSlot.Hat,
            EquipmentSlot.Coat,
            EquipmentSlot.WeaponLeft,
            EquipmentSlot.WeaponRight,
            EquipmentSlot.Foot
        };
        public static string GetSlotName(EquipmentSlot slot)
        {
            switch (slot)
            {
                case EquipmentSlot.Weapon:
                    return "手持";
                case EquipmentSlot.All:
                    return "任意";
            }
            var ret = "";
            foreach (var availableSlot in AvailableEquipmentSlots.Where(availableSlot => slot.HasFlag(availableSlot)))
            {
                if (ret.Length > 0)
                {
                    ret += ", ";
                }
                ret += GetSlotName_Base(availableSlot);
            }
            return ret;
        }
        private static string GetSlotName_Base(EquipmentSlot slot)
        {
            return slot switch {
                EquipmentSlot.Hat => "帽子",
                EquipmentSlot.Coat => "衣服",
                EquipmentSlot.WeaponLeft => "左手",
                EquipmentSlot.WeaponRight => "右手",
                EquipmentSlot.Foot => "鞋子",
                _ => "",
            };
        }
        public static EquipmentBase SpawnEquipment(string name)
        {
            var equip = TrySpawnEquipment(name);
            if (equip == null)
            {
                Debug.LogError("Equipment not found: " + name);
            }
            return equip;
        }
        public static EquipmentBase TrySpawnEquipment(string name)
        {
            AllEquipmentSpawner.TryGetValue(name, out EquipmentBase spawnInfo);
            if (spawnInfo == null)
            {
                return null;
            }
            return spawnInfo.CreateEquipmentInstance();
        }
        public static void RegisterAllEquipmentInfo()
        {
            RegisterAllLuaEquipmentInfo();
            AllEquipmentSpawner.Add(EmptyEquipment, new EquipmentEmpty());
        }
        private static void RegisterAllLuaEquipmentInfo()
        {
            LuaManager.RegisterAllLuaScriptsOf("Equipments", (id, path, module) =>
            {
                AllEquipmentSpawner.Add(
                    id,
                    new EquipmentLua(module)
                );
            });
        }
    }
}