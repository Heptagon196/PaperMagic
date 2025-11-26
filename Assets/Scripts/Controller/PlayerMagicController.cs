using System;
using System.Collections.Generic;
using Backpack;
using Equipment;
using Spell;
using UI;
using UI.General;
using UI.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    public class PlayerMagicController : MonoBehaviour, IPropertyPercent
    {
        public static PlayerMagicController Instance;
        private readonly Dictionary<EquipmentSlot, EquipmentBase> _currentEquip= new();

        private readonly Dictionary<EquipmentSlot, Func<bool>> _controlMethods = new()
        {
            { EquipmentSlot.Hat, () => Input.GetKey(KeyCode.Q) },
            { EquipmentSlot.Coat, () => Input.GetKey(KeyCode.E) },
            { EquipmentSlot.WeaponLeft, () => Input.GetMouseButton(0) },
            { EquipmentSlot.WeaponRight, () => Input.GetMouseButton(1) },
            { EquipmentSlot.Foot, () => Input.GetKey(KeyCode.Space) },
        };
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                EventManager.AddListener(this, UIPanelEvent.CloseUI, _ => GenerateSpellTree());
                PlayerPropertySlider.RegisterProperty(SliderPropertyType.Hat, this);
                PlayerPropertySlider.RegisterProperty(SliderPropertyType.Coat, this);
                PlayerPropertySlider.RegisterProperty(SliderPropertyType.Left, this);
                PlayerPropertySlider.RegisterProperty(SliderPropertyType.Right, this);
                PlayerPropertySlider.RegisterProperty(SliderPropertyType.Foot, this);
            }
        }
        private void OnDestroy()
        {
            EventManager.RemoveListeners(this, UIPanelEvent.CloseUI);
        }
        private void Start()
        {
            GenerateSpellTree();
        }
        private void Update()
        {
            foreach (var controlMethod in _controlMethods)
            {
                if (_currentEquip.TryGetValue(controlMethod.Key, out var equipment))
                {
                    equipment.ResumeManaOnUpdate();
                }
            }
            if (PlayerController.Instance.movementMode == PlayerMovementMode.Topdown)
            {
                return;
            }
            if (UIFunctions.Instance.UIOpen || CheckMouseHover.MouseOnUI)
            {
                return;
            }
            foreach (var controlMethod in _controlMethods)
            {
                if (_currentEquip.TryGetValue(controlMethod.Key, out var equipment))
                {
                    switch (equipment.castType)
                    {
                        case EquipmentCastType.Automatic:
                            equipment.ExecuteSpell(PlayerController.Instance);
                            break;
                        case EquipmentCastType.PressKey:
                            if (controlMethod.Value.Invoke())
                            {
                                equipment.ExecuteSpell(PlayerController.Instance);
                            }
                            break;
                    }
                }
            }
        }
        private void GenerateSpellTree()
        {
            _currentEquip.Clear();
            foreach (var equipSlot in EquipmentManager.AvailableEquipmentSlots)
            {
                var equippedInfo = BackpackManager.Instance.GetEquipped(equipSlot);
                if (equippedInfo == null)
                {
                    continue;
                }
                var equip = EquipmentManager.TrySpawnEquipment(equippedInfo.equipmentID);
                if (equip == null)
                {
                    continue;
                }
                equip.InitWeapon(GetComponent<PlayerController>());
                var spellTree = equippedInfo.CreateSpellTree();
                if (spellTree.GetSize() > equip.equipmentCapacity && equip.equipmentCapacity > 0)
                {
                    UIFunctions.Instance.ShowFloatTip(
                        $"{EquipmentManager.GetSlotName(equipSlot)}处法术数量超过上限：{spellTree.GetSize()} > {equip.equipmentCapacity}");
                    spellTree = new SpellTreeBaseEmpty();
                }
                equip.InitSpellTree(spellTree);
                _currentEquip.Add(equipSlot, equip);
            }
        }
        public int SpellEquippedCount(int slot, string checkSpellID)
        {
            int count = 0;
            var equippedInfo = BackpackManager.Instance.GetEquipped((EquipmentSlot)slot);
            equippedInfo.CurrentScheme.schemeData.ForEach(colData =>
                colData.columnData.ForEach(spellID =>
                {
                    if (spellID == checkSpellID)
                    {
                        count++;
                    }
                })
            );
            return count;
        }
        private EquipmentBase GetEquipment(EquipmentSlot slot)
        {
            return _currentEquip.GetValueOrDefault(slot, null);
        }
        public float GetPropertyPercent(SliderPropertyType propertyType)
        {
            return GetEquipment(propertyType switch
            {
                SliderPropertyType.Hat => EquipmentSlot.Hat,
                SliderPropertyType.Coat => EquipmentSlot.Coat,
                SliderPropertyType.Left => EquipmentSlot.WeaponLeft,
                SliderPropertyType.Right => EquipmentSlot.WeaponRight,
                SliderPropertyType.Foot => EquipmentSlot.Foot,
                _ => EquipmentSlot.None
            })?.ManaPercent ?? 0;
        }
    }
}