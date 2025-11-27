using System;
using System.Collections.Generic;
using Controller;
using Equipment;
using SaveData;
using Spell;
using UI;
using UI.General;
using UnityEngine;

namespace Backpack
{
    public enum BackpackEvent
    {
        BackpackChanged,
        EquipChanged,
    }
    public class BackpackManager : MonoBehaviour, ISaveDataProcesser
    {
        private static BackpackManager _instance;
        public static BackpackManager Instance => _instance;
        private readonly Dictionary<BackpackSlot, Dictionary<string, int>> _data = new();
        private readonly Dictionary<BackpackSlot, Dictionary<string, IBackpackItemInfo>> _cachedItemInfos = new();
        private readonly Dictionary<EquipmentSlot, EquippedItemInfo> _equipped = new();
        private readonly Dictionary<BackpackSlot, IBackpackItemInfo> _spawners = new()
        {
            { BackpackSlot.Equipment, new EquipmentInfo() },
            { BackpackSlot.Spell, new SpellInfo() },
            { BackpackSlot.Item, new NormalItemInfo() },
        };
        private bool _inited = false;
        private void InitData()
        {
            if (_inited)
            {
                return;
            }
            _inited = true;
            foreach (var spawner in _spawners)
            {
                _data.Add(spawner.Key, new());
                _cachedItemInfos.Add(spawner.Key, new());
            }
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                InitData();
                return;
            }
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        public Dictionary<string, int> GetData(BackpackSlot slot)
        {
            _data.TryGetValue(slot, out var ret);
            return ret;
        }
        public void SetNum(BackpackSlot slot, string key, int num, bool notify = true)
        {
            var slotData = GetData(slot);
            if (slotData != null)
            {
                if (num <= 0)
                {
                    slotData.Remove(key);
                }
                else
                {
                    slotData.TryAdd(key, num);
                    slotData[key] = num;
                }
                if (notify)
                {
                    EventManager.Broadcast(BackpackEvent.BackpackChanged);
                }
            }
        }
        public void AddNum(BackpackSlot slot, string key, int num, bool notify = true)
        {
            var slotData = GetData(slot);
            if (slotData != null)
            {
                slotData.TryAdd(key, 0);
                slotData[key] += num;
                if (slotData[key] <= 0)
                {
                    slotData.Remove(key);
                }
                if (notify)
                {
                    EventManager.Broadcast(BackpackEvent.BackpackChanged);
                }
            }
        }
        public int GetNum(BackpackSlot slot, string key)
        {
            var slotData = GetData(slot);
            if (slotData != null)
            {
                slotData.TryAdd(key, 0);
                return slotData[key];
            }
            return 0;
        }
        public Dictionary<EquipmentSlot, EquippedItemInfo> GetEquipped()
        {
            return _equipped;
        }
        public EquippedItemInfo GetEquipped(EquipmentSlot slot)
        {
            if (!_equipped.ContainsKey(slot))
            {
                _equipped.Add(slot, EquippedItemInfo.Empty());
            }
            _equipped.TryGetValue(slot, out var result);
            return result;
        }
        public EquippedItemInfo GetOrAddEquipped(EquipmentSlot slot)
        {
            if (!_equipped.ContainsKey(slot))
            {
                _equipped.Add(slot, EquippedItemInfo.Empty());
            }
            _equipped.TryGetValue(slot, out var result);
            if (result == null)
            {
                var newItemInfo = new EquippedItemInfo
                {
                    equipmentID = EquipmentManager.EmptyEquipment
                };
                _equipped.Add(slot, newItemInfo);
                return newItemInfo;
            }
            return result;
        }
        public void Equip(EquipmentSlot slot, string id)
        {
            var info = GetItemInfo(BackpackSlot.Equipment, id) as EquipmentInfo;
            if (info == null)
            {
                return;
            }
            if ((info.slot & slot) == 0)
            {
                UIFunctions.Instance.ShowFloatTip("该装备无法装备到此位置");
                return;
            }
            if (!_equipped.ContainsKey(slot))
            {
                _equipped.Add(slot, EquippedItemInfo.Empty());
            }
            var toDel = _equipped[slot].equipmentID;
            if (toDel != EquipmentManager.EmptyEquipment)
            {
                AddNum(BackpackSlot.Equipment, toDel, 1, false);
            }
            if (id != EquipmentManager.EmptyEquipment)
            {
                AddNum(BackpackSlot.Equipment, id, -1, false);
            }
            _equipped[slot].equipmentID = id;
            EventManager.Broadcast(BackpackEvent.BackpackChanged);
            EventManager.Broadcast(BackpackEvent.EquipChanged);
        }
        public IBackpackItemInfo GetItemInfo(BackpackSlot slot, string id)
        {
            if (!_cachedItemInfos.TryGetValue(slot, out var list))
            {
                return null;
            }
            if (list.TryGetValue(id, out var info))
            {
                return info;
            }
            if (_spawners.TryGetValue(slot, out var spawner))
            {
                var newCache = spawner.SpawnNew(id);
                list.Add(id, newCache);
                return newCache;
            }

            return null;
        }
        public void SaveDataTo(ref GameData gameData)
        {
            gameData.backpack.Clear();
            foreach (var spawner in _spawners)
            {
                foreach (var slot in GetData(spawner.Key))
                {
                    gameData.backpack.Add(new GameDataItem
                    {
                        type = spawner.Key,
                        id = slot.Key,
                        count = slot.Value
                    });
                }
            }
            gameData.equipped.Clear();
            foreach (var equipment in _equipped)
            {
                gameData.equipped.Add(new GameDataEquipment
                {
                    slot = equipment.Key,
                    info = equipment.Value
                });
            }
        }
        public void LoadDataFrom(ref GameData gameData)
        {
            InitData();
            foreach (var spawner in _spawners)
            {
                var curData = GetData(spawner.Key);
                curData.Clear();
            }
            foreach (var slot in gameData.backpack)
            {
                GetData(slot.type).Add(slot.id, slot.count);
            }
            _equipped.Clear();
            foreach (var equipment in gameData.equipped)
            {
                var data = equipment.info;
                if (string.IsNullOrEmpty(data.equipmentID))
                {
                    data.equipmentID = EquipmentManager.EmptyEquipment;
                }
                _equipped.Add(equipment.slot, equipment.info);
            }
        }
        public void SetDefaultData(ref GameData gameData)
        {
            gameData.backpack.Clear();
            gameData.equipped.Clear();
            /*
            gameData.backpack.Add(new GameDataItem
            {
                type = BackpackSlot.Equipment,
                id = "std.default",
                count = 1
            });
            gameData.backpack.Add(new GameDataItem
            {
                type = BackpackSlot.Spell,
                id = "std.default",
                count = 1
            });
            var defaultEquip = new EquippedItemInfo
            {
                equipmentID = "std.default",
                activeScheme = 0,
                availableSchemes = new()
                {
                    new SpellTreeSchemeData()
                    {
                        schemeName = "defaultScheme",
                        schemeData = new()
                        {
                            new()
                            {
                                columnData = new()
                                {
                                    "std.default"
                                }
                            }
                        }
                    }
                }
            };
            gameData.equipped.Add(new GameDataEquipment()
            {
                slot = EquipmentSlot.WeaponLeft,
                info = defaultEquip
            });
            */
        }
    }
}