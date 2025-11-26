using System;
using PMLua;
using XLua;

namespace Equipment
{
    public class EquipmentLua : EquipmentBase
    {
        private readonly Action _onAdd;
        private readonly Action _onDel;
        public EquipmentLua(LuaTable module)
        {
            if (module == null)
            {
                return;
            }
            equipmentID = module.Get<string>("ID") ?? "";
            equipmentName = module.Get<string>("Name") ?? "";
            equipmentDesc = module.Get<string>("Desc") ?? "";
            equipmentIcon = module.Get<string>("Icon") ?? "";
            equipmentCapacity = module.TryGet<int>("Capacity", 0);
            slot = (EquipmentSlot)module.TryGet<int>("Slot", 0);
            castType = (EquipmentCastType)module.TryGet<int>("CastType", 0);
            maxMana = module.TryGet<float>("MaxMana", 0);
            manaResumePerSecond = module.TryGet<float>("ManaResume", 0);
            minCastInterval = module.TryGet<float>("CastInterval", 0);
            
            _onAdd = module.Get<Action>("OnAdd");
            _onDel = module.Get<Action>("OnDel");
        }
        public override void OnAdd()
        {
            _onAdd?.Invoke();
        }
        public override void OnDel()
        {
            _onDel?.Invoke();
        }
    }
}