using System;
using System.Collections.Generic;
using Equipment;
using Spell;
using Unity.VisualScripting;
using UnityEngine;

namespace Backpack
{
    public enum BackpackSlot
    {
        Equipment,
        Spell,
        Item,
    }
    public interface IBackpackItemInfo
    {
        public string GetName();
        public string GetDesc();
        public string GetIconPath();
        public IBackpackItemInfo SpawnNew(string id);
    }
    [Serializable]
    public class NormalItemInfo : IBackpackItemInfo
    {
        public string id;
        public string name;
        public string desc;
        public string iconPath;
        public NormalItemType type;
        private NormalItemBase _refItemBase;
        public int UseItem() => _refItemBase.UseItem();
        public string GetName() => name;
        public string GetDesc() => desc;
        public string GetIconPath() => iconPath;
        public NormalItemInfo() {}
        public NormalItemInfo(NormalItemBase normalItemBase)
        {
            _refItemBase = normalItemBase;
            id = normalItemBase.itemID;
            name = normalItemBase.itemName;
            desc = normalItemBase.itemDesc;
            iconPath = normalItemBase.itemIcon;
            type = normalItemBase.itemType;
        }

        public IBackpackItemInfo SpawnNew(string inID)
        {
            var info = NormalItemManager.SpawnItem(inID);
            return info != null ? new NormalItemInfo(info) : null;
        }
    }
    [Serializable]
    public class EquipmentInfo : IBackpackItemInfo
    {
        public string id;
        public string equipmentName;
        public string equipmentDesc;
        public int equipmentCapacity;
        public Equipment.EquipmentSlot slot;
        public Equipment.EquipmentCastType castType;
        public float maxMana;
        public float manaResumePerSecond;
        public float minCastInterval;
        // public Sprite equipmentIcon;
        public string equipmentIcon;
        public EquipmentInfo() {}
        public EquipmentInfo(Equipment.EquipmentBase equipmentBase)
        {
            id = equipmentBase.equipmentID;
            equipmentName = equipmentBase.equipmentName;
            equipmentDesc = equipmentBase.equipmentDesc;
            equipmentCapacity = equipmentBase.equipmentCapacity;
            slot = equipmentBase.slot;
            castType = equipmentBase.castType;
            maxMana = equipmentBase.maxMana;
            manaResumePerSecond = equipmentBase.manaResumePerSecond;
            minCastInterval = equipmentBase.minCastInterval;
            equipmentIcon = equipmentBase.equipmentIcon;
            // equipmentIcon = IconLoader.GetIcon(equipmentBase.equipmentIcon);
        }
        public string GetCastTypeDisplay()
        {
            return castType switch
            {
                EquipmentCastType.Automatic => "自动",
                EquipmentCastType.Passive => "被动",
                EquipmentCastType.PressKey => "主动",
                _ => ""
            };
        }
        public IBackpackItemInfo SpawnNew(string inID)
        {
            return new EquipmentInfo(EquipmentManager.SpawnEquipment(inID));
        }
        public string GetName() => equipmentName;
        public string GetIconPath() => equipmentIcon;
        public string GetDesc()
        {
            return
@$"<size=20><color=Black>{equipmentName}</color></size>

<color=Grey>{equipmentDesc}</color>

<color=Black>施法方式：<color=Green>  {GetCastTypeDisplay()}</color>
法术上限：<color=Green>  {equipmentCapacity}</color>
最大法力：<color=Green>  {maxMana}</color>
恢复速度：<color=Green>  {manaResumePerSecond}</color>
施法间隔：<color=Green>  {minCastInterval}</color></color>";
        }
    }
    [Serializable]
    public class SpellInfo : IBackpackItemInfo
    {
        public string id;
        public int maxChildNodeCount;
        public float spellCost;
        public float spellDamage;
        public Spell.SpellType spellType;
        public string spellName;
        public string spellDesc;
        // public Sprite spellIcon;
        public string spellIcon;
        public SpellInfo() {}
        public SpellInfo(Spell.SpellTreeBase spellTreeBase)
        {
            id = spellTreeBase.id;
            maxChildNodeCount = spellTreeBase.maxChildNodeCount;
            spellCost = spellTreeBase.spellCost;
            spellDamage = spellTreeBase.spellDamage;
            spellType = spellTreeBase.spellType;
            spellName = spellTreeBase.spellName;
            spellDesc = spellTreeBase.spellDesc;
            spellIcon = spellTreeBase.iconPath;
            // spellIcon = IconLoader.GetIcon(spellTreeBase.iconPath);
        }
        public string GetSpellTypeDisplay()
        {
            return spellType switch
            {
                SpellType.Projectile => "投射物",
                SpellType.Modifier => "修饰法术",
                SpellType.Other => "特殊法术",
                _ => ""
            };
        }
        public string GetSpellDamageDisplay()
        {
            if (spellDamage == 0)
            {
                return "";
            }
            return spellType switch
            {
                SpellType.Modifier => $"\n增加伤害：<color=Green>  {spellDamage}</color>",
                _ => $"\n造成伤害：<color=Green>  {spellDamage}</color>"
            };
        }
        public IBackpackItemInfo SpawnNew(string inID)
        {
            var info = SpellManager.SpawnSpell(inID);
            return info != null ? new SpellInfo(info) : null;
        }
        public string GetName() => spellName;
        public string GetIconPath() => spellIcon;
        public string GetDesc()
        {
            return
@$"<size=20><color=Black>{spellName}</color></size>

<color=Grey>{spellDesc}</color>

<color=Black>法术类型：<color=Green>  {GetSpellTypeDisplay()}</color>{GetSpellDamageDisplay()}
消耗法力：<color=Green>  {spellCost}</color></color>";
        }
    }
    [Serializable]
    public class EquippedItemInfo
    {
        public string equipmentID;
        public int activeScheme;
        public List<SpellTreeSchemeData> availableSchemes = new();
        public SpellTreeSchemeData CurrentScheme => availableSchemes[activeScheme];
        public SpellTreeBase CreateSpellTree()
        {
            return activeScheme < availableSchemes.Count ? availableSchemes[activeScheme].CreateSpellTree() : null;
        }
        public static EquippedItemInfo Empty()
        {
            return new EquippedItemInfo()
            {
                equipmentID = EquipmentManager.EmptyEquipment,
                activeScheme = 0,
                availableSchemes = new List<SpellTreeSchemeData>()
                {
                    new SpellTreeSchemeData()
                    {
                        schemeName = "",
                        schemeData = new List<SpellTreeSchemeColumnData>()
                        {
                            new SpellTreeSchemeColumnData()
                            {
                                columnData = new List<string>()
                                {
                                    SpellManager.EmptySpell
                                }
                            }
                        }
                    }
                }
            };
        }
    }
    [Serializable]
    public class SpellTreeSchemeColumnData
    {
        public List<string> columnData = new();
        public int GetNextColumnMaxSpellNum()
        {
            int count = 0;
            foreach (var data in columnData)
            {
                var info = BackpackManager.Instance.GetItemInfo(BackpackSlot.Spell, data) as SpellInfo;
                count += info?.maxChildNodeCount ?? 0;
            }
            return count;
        }
    }
    [Serializable]
    public class SpellTreeSchemeData
    {
        public string schemeName;
        public List<SpellTreeSchemeColumnData> schemeData = new();
        public void StandardizeScheme()
        {
            if (schemeData.Count == 0 ||
                (schemeData.Count > 0 && schemeData[^1].GetNextColumnMaxSpellNum() > 0))
            {
                schemeData.Add(new SpellTreeSchemeColumnData()
                {
                    columnData = new()
                    {
                        SpellManager.EmptySpell
                    }
                });
            }
            int maxColCount = schemeData.Count;
            int currentColMaxNum = 1;
            for (var col = 0; col < schemeData.Count; col++)
            {
                var colData = schemeData[col].columnData;
                while (colData.Count < currentColMaxNum)
                {
                    colData.Add(SpellManager.EmptySpell);
                }
                if (colData.Count > currentColMaxNum)
                {
                    colData.RemoveRange(currentColMaxNum, colData.Count - currentColMaxNum);
                }
                currentColMaxNum = schemeData[col].GetNextColumnMaxSpellNum();
                if (currentColMaxNum == 0)
                {
                    maxColCount = Math.Min(col + 1, schemeData.Count);
                    break;
                }
            }
            if (maxColCount < schemeData.Count)
            {
                schemeData.RemoveRange(maxColCount, schemeData.Count - maxColCount);
            }
        }
        struct SpellTreeQueueData
        {
            public SpellTreeBase Tree;
            public int Depth;
        }
        public void ReadFromSpellTree(SpellTreeBase spellTreeBase)
        {
            schemeData = new();
            var curRoot = spellTreeBase;
            Queue<SpellTreeQueueData> queue = new();
            queue.Enqueue(new SpellTreeQueueData()
            {
                Tree = curRoot,
                Depth = 0,
            });
            while (queue.Count > 0)
            {
                SpellTreeQueueData data = queue.Dequeue();
                int depth = data.Depth + 1;
                List<string> columnData = new();
                foreach (var node in data.Tree.Nodes)
                {
                    columnData.Add(node.id);
                    foreach (var childNode in node.Nodes)
                    {
                        queue.Enqueue(new SpellTreeQueueData()
                        {
                            Tree = childNode,
                            Depth = depth,
                        });
                    }
                }
                schemeData.Add(new ()
                {
                    columnData = columnData
                });
            }
        }
        public Dictionary<Vector2Int, List<Vector2Int>> GetSpellTreeConnections(out int lineCount)
        {
            var connections = new Dictionary<Vector2Int, List<Vector2Int>>();
            lineCount = 0;
            Queue<SpellInfo> prevCol = new();
            int headRemainCount = 1;
            var root = new SpellInfo();
            prevCol.Enqueue(root);
            int currentPrevRow = 0;
            for (int currentCol = 0; currentCol < schemeData.Count; currentCol++)
            {
                var colData = schemeData[currentCol];
                Queue<SpellInfo> thisCol = new();
                for (int currentRow = 0; currentRow < colData.columnData.Count; currentRow++)
                {
                    var spellID = colData.columnData[currentRow];
                    while (headRemainCount == 0 && prevCol.Count > 0)
                    {
                        var current = prevCol.Dequeue();
                        headRemainCount = current.maxChildNodeCount;
                        currentPrevRow++;
                    }
                    if (headRemainCount > 0)
                    {
                        headRemainCount--;
                        var newSpellInfo = BackpackManager.Instance.GetItemInfo(BackpackSlot.Spell, spellID) as SpellInfo;
                        thisCol.Enqueue(newSpellInfo);
                        if (currentCol > 0)
                        {
                            var start = new Vector2Int(currentCol - 1, currentPrevRow);
                            var end = new Vector2Int(currentCol, currentRow);
                            if (!connections.ContainsKey(start))
                            {
                                connections.Add(start, new List<Vector2Int>());
                            }
                            connections[start].Add(end);
                            lineCount++;
                        }
                    }
                }
                prevCol = thisCol;
                if (prevCol.Count > 0)
                {
                    var current = prevCol.Dequeue();
                    headRemainCount = current.maxChildNodeCount;
                }
                currentPrevRow = 0;
            }
            return connections;
        }
        public SpellTreeBase CreateSpellTree()
        {
            var root = new SpellTreeBaseVirtualRoot();
            Queue<SpellTreeBase> prevCol = new();
            int headRemainCount = 1;
            prevCol.Enqueue(root);
            SpellTreeBase current = root;
            foreach (var colData in schemeData)
            {
                Queue<SpellTreeBase> thisCol = new();
                foreach (var spellID in colData.columnData)
                {
                    while (headRemainCount == 0 && prevCol.Count > 0)
                    {
                        current = prevCol.Dequeue();
                        headRemainCount = current.maxChildNodeCount;
                    }
                    if (headRemainCount > 0)
                    {
                        headRemainCount--;
                        var newSpell = SpellManager.SpawnSpell(spellID);
                        current.Nodes.Add(newSpell);
                        thisCol.Enqueue(newSpell);
                    }
                }
                prevCol = thisCol;
                if (prevCol.Count > 0)
                {
                    current = prevCol.Dequeue();
                    headRemainCount = current.maxChildNodeCount;
                }
            }
            return root;
        }
    }
}