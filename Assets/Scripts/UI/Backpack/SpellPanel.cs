using System;
using System.Collections.Generic;
using System.Linq;
using Backpack;
using Controller;
using Equipment;
using Spell;
using UI.General;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Backpack
{
    public struct SpellPanelItemData : IItemButtonExtraData
    {
        public List<SpellTreeSchemeColumnData> SchemeData;
        public int Col;
        public int Row;
        public string GetSpell()
        {
            return SchemeData[Col].columnData[Row];
        }
        public void SetSpell(string newSpell)
        {
            SchemeData[Col].columnData[Row] = newSpell;
        }
    }
    public class SpellPanel : MonoBehaviour
    {
        public GameObject content;
        public GameObject spellLinePrefab;
        public GameObject spellItemPrefab;
        public GameObject spellConnectionContainer;
        public GameObject spellConnectLinePrefab;
        public RectTransform viewportRect;
        public float lineThickness = 5f;
        public EquippedEquipmentPanel equippedPanel;
        private static Dictionary<Vector2Int, List<Vector2Int>> _connectionInfo;
        private Canvas _canvas;
        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            Refresh();
            equippedPanel.OnSwitchSlot = _ =>
            {
                Refresh();
            };
        }
        private Transform GetColumnTransform(int colID)
        {
            return content.transform.GetChild(colID).GetComponent<SpellColumn>().content.transform;
        }
        private ItemButton GetItem(int colID, int rowID)
        {
            var colTransform = GetColumnTransform(colID);
            return colTransform.GetChild(rowID).GetComponent<ItemButton>();
        }
        private Image GetConnectLine(int id)
        {
            return spellConnectionContainer.transform.GetChild(id).GetComponent<Image>();
        }
        private static int GetSpellChildNodeCount(string spell)
        {
            return (BackpackManager.Instance.GetItemInfo(BackpackSlot.Spell, spell) as SpellInfo)?.maxChildNodeCount ?? 0;
        }
        private void OnAddSpell(string spellID)
        {
            if (spellID != SpellManager.EmptySpell)
            {
                BackpackManager.Instance.AddNum(BackpackSlot.Spell, spellID, -1, false);
            }
        }
        private void OnDelSpell(string spellID)
        {
            if (spellID != SpellManager.EmptySpell)
            {
                BackpackManager.Instance.AddNum(BackpackSlot.Spell, spellID, 1, false);
            }
        }
        private void ReplaceSpellImpl(ItemButton self, string newSpell)
        {
            if (self.stat == ItemStat.SpellPanel)
            {
                if (self.ExtraData is SpellPanelItemData data)
                {
                    int childCountDiff = GetSpellChildNodeCount(newSpell) - GetSpellChildNodeCount(data.GetSpell());
                    OnDelSpell(data.GetSpell());
                    OnAddSpell(newSpell);
                    data.SetSpell(newSpell);
                    var prevRowBottomConnectPos = -1;
                    if (data.Row > 0)
                    {
                        for (var row = 0; row < data.Row; row++)
                        {
                            _connectionInfo.TryGetValue(new Vector2Int(data.Col, row), out var prevRowConnections);
                            if (prevRowConnections != null)
                            {
                                prevRowBottomConnectPos = prevRowConnections.Select(pos => pos.y).Prepend(prevRowBottomConnectPos).Max();
                            }
                        }
                    }
                    _connectionInfo.TryGetValue(new Vector2Int(data.Col, data.Row), out var currentColConnected);
                    currentColConnected ??= new();
                    var currentColID = data.Col + 1;
                    while (childCountDiff != 0)
                    {
                        var nextCol = new List<Vector2Int>();
                        int nextChildCountDiff = 0;
                        var bottomPos = -1;
                        foreach (var pos in currentColConnected)
                        {
                            bottomPos = Math.Max(bottomPos, pos.y);
                            _connectionInfo.TryGetValue(pos, out var nextColPart);
                            if (nextColPart != null)
                            {
                                nextCol.AddRange(nextColPart);
                            }
                        }
                        if (childCountDiff > 0)
                        {
                            var startPos = bottomPos == -1 ? prevRowBottomConnectPos + 1 : bottomPos + 1;
                            while (currentColID > data.SchemeData.Count - 1)
                            {
                                data.SchemeData.Add(new SpellTreeSchemeColumnData()
                                {
                                    columnData = new()
                                });
                            }
                            for (var count = 0; count < childCountDiff; count++)
                            {
                                data.SchemeData[currentColID].columnData.Insert(startPos, SpellManager.EmptySpell);
                            }
                        }
                        else
                        {
                            var delPos = bottomPos;
                            for (var count = 0; count < -childCountDiff; count++)
                            {
                                var columnData = data.SchemeData[currentColID].columnData;
                                nextChildCountDiff -= GetSpellChildNodeCount(columnData[delPos]);
                                OnDelSpell(columnData[delPos]);
                                columnData.RemoveAt(delPos);
                                delPos--;
                            }
                        }

                        currentColConnected = nextCol;
                        childCountDiff = nextChildCountDiff;
                        currentColID++;
                    }
                    Refresh();
                    EventManager.Broadcast(BackpackEvent.BackpackChanged);
                }
            }
        }
        private void OnRemoveSpell(ItemButton self)
        {
            if (self == null)
            {
                return;
            }
            ReplaceSpellImpl(self, SpellManager.EmptySpell);
        }
        private void OnReplaceSpell(ItemButton self, ItemButton other)
        {
            if (self == null || other == null)
            {
                return;
            }
            if ((other.stat == ItemStat.Backpack || other.stat == ItemStat.SpellPanel) &&
                other.itemType == BackpackSlot.Spell)
            {
                ReplaceSpellImpl(self, other.itemID);
            }
        }

        private void Refresh()
        {
            var equipData = BackpackManager.Instance.GetOrAddEquipped(equippedPanel.CurrentSelectedSlot);
            if (equipData.availableSchemes.Count == 0)
            {
                equipData.availableSchemes.Add(new SpellTreeSchemeData()
                {
                    schemeName = "default",
                });
                equipData.activeScheme = 0;
            }

            equipData.CurrentScheme.StandardizeScheme();
            var scheme = equipData.CurrentScheme.schemeData;
            UIFunctions.ResizeContainer(content.transform, spellLinePrefab, scheme.Count, null);
            int maxRowCount = 0;
            for (var col = 0; col < scheme.Count; col++)
            {
                var colData = scheme[col].columnData;
                var colTransform = GetColumnTransform(col);
                UIFunctions.ResizeContainer(colTransform, spellItemPrefab, colData.Count, child =>
                {
                    var item = child.GetComponent<ItemButton>();
                    item.Init(ItemStat.SpellPanel, true, true, true, true);
                    item.OnDragFrom = OnReplaceSpell;
                    item.OnRightClick = OnRemoveSpell;
                    item.EnableOutline();
                });
                maxRowCount = Math.Max(maxRowCount, colData.Count);
                for (var row = 0; row < colData.Count; row++)
                {
                    var button = GetItem(col, row);
                    button.ExtraData = new SpellPanelItemData()
                    {
                        SchemeData = scheme,
                        Col = col,
                        Row = row,
                    };
                    string desc = null;
                    if (colData[row] == SpellManager.EmptySpell)
                    {
                        desc = "拖拽到此处添加";
                    }
                    button.LoadInfo(BackpackSlot.Spell, colData[row], 1, desc);
                }
            }

            viewportRect.sizeDelta = new Vector2(scheme.Count * 68, maxRowCount * 58);
            LayoutRebuilder.ForceRebuildLayoutImmediate(viewportRect);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
            for (var col = 0; col < scheme.Count; col++)
            {
                var colTransform = GetColumnTransform(col);
                LayoutRebuilder.ForceRebuildLayoutImmediate(colTransform.GetComponent<RectTransform>());
                LayoutRebuilder.ForceRebuildLayoutImmediate(colTransform.parent.GetComponent<RectTransform>());
            }

            _connectionInfo = equipData.CurrentScheme.GetSpellTreeConnections(out var connectionsCount);
            UIFunctions.ResizeContainer(spellConnectionContainer.transform, spellConnectLinePrefab, connectionsCount, null);
            var lineID = 0;
            foreach (var connection in _connectionInfo)
            {
                var startPos = connection.Key;
                foreach (var endPos in connection.Value)
                {
                    var line = GetConnectLine(lineID);
                
                    var start = GetItem(startPos.x, startPos.y).GetComponent<RectTransform>();
                    var end = GetItem(endPos.x, endPos.y).GetComponent<RectTransform>();
                
                    var distance = Vector2.Distance(start.position, end.position);
                    var angle = Vector2.SignedAngle(start.position - end.position, Vector2.left);
                    line.GetComponent<RectTransform>().anchoredPosition = (start.anchoredPosition + end.anchoredPosition) / 2;
                    line.transform.position = (start.position + end.position) / 2;
                    line.GetComponent<RectTransform>().sizeDelta = new Vector2(distance / _canvas.scaleFactor, lineThickness);
                    line.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
                    lineID++;
                }
            }
        }
    }
}
