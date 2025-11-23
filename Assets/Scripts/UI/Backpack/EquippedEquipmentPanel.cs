using System;
using Backpack;
using Controller;
using Equipment;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Backpack
{
    [Serializable]
    public struct SpellEquipmentInfo
    {
        public string name;
        public EquipmentSlot slot;
        public ItemButton button;
    }
    public struct EquipPanelItemData : IItemButtonExtraData
    {
        public EquipmentSlot Slot;
    }
    public class EquippedEquipmentPanel : MonoBehaviour
    {
        public SpellEquipmentInfo[] spellEquipmentInfo;
        public int currentSelected = 0;
        public EquipmentSlot CurrentSelectedSlot => spellEquipmentInfo[currentSelected].slot;
        public bool enableDrop = false;
        public bool enableRightClick = false;
        public bool enableTipOnEmpty = false;
        public Text showCurrentSelected;
        public Action<int> OnSwitchSlot = null;
        private void Awake()
        {
            EventManager.AddListener(this, BackpackEvent.EquipChanged, _ => RefreshPanel());
        }
        private void OnDestroy()
        {
            EventManager.RemoveListeners(this, BackpackEvent.EquipChanged);
        }
        private void Equip(ItemButton self, string target)
        {
            if (self.ExtraData is EquipPanelItemData data)
            {
                BackpackManager.Instance.Equip(data.Slot, target);
                RefreshPanel();
            }
        }
        private void Equip(ItemButton self, ItemButton other)
        {
            if (self == null || other == null)
            {
                return;
            }
            if (other.stat == ItemStat.Backpack &&
                other.itemType == BackpackSlot.Equipment)
            {
                Equip(self, other.itemID);
            }
        }
        public void Start()
        {
            var idx = 0;
            foreach (var equipmentInfo in spellEquipmentInfo)
            {
                var item = equipmentInfo.button;
                item.Init(ItemStat.Equipped, false, enableDrop, true, enableRightClick);
                item.ExtraData = new EquipPanelItemData()
                {
                    Slot = equipmentInfo.slot
                };
                item.OnDragFrom = Equip;
                item.OnRightClick = button => Equip(button, EquipmentManager.EmptyEquipment);
                var equipmentIdx = idx;
                idx++;
                item.button.onClick.AddListener(() =>
                {
                    currentSelected = equipmentIdx;
                    OnSwitchSlot?.Invoke(equipmentIdx);
                    showCurrentSelected.text = equipmentInfo.name;
                    foreach (var info in spellEquipmentInfo)
                    {
                        info.button.EnableOutline(info.slot == equipmentInfo.slot);
                    }
                });
            }
            RefreshPanel();
        }
        public void RefreshPanel()
        {
            for (int idx = 0; idx < spellEquipmentInfo.Length; idx++)
            {
                var equipmentInfo =  spellEquipmentInfo[idx];
                equipmentInfo.button.GetComponent<Outline>().enabled = idx == currentSelected;
                var item = equipmentInfo.button;
                var equipID = BackpackManager.Instance.GetOrAddEquipped(equipmentInfo.slot)?.equipmentID;
                string desc = null;
                
                if (equipID == EquipmentManager.EmptyEquipment)
                {
                    desc = $"{EquipmentManager.GetSlotName(equipmentInfo.slot)}\n<color=Grey>拖拽到此处装备</color>";
                }

                item.LoadInfo(BackpackSlot.Equipment, equipID, 1, desc);
                if (!enableTipOnEmpty)
                {
                    item.enableTip = equipID != EquipmentManager.EmptyEquipment;
                }
            }
        }
    }
}