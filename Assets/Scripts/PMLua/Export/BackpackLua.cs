using Backpack;
using Equipment;
using XLua;

namespace PMLua.Export
{
    [LuaCallCSharp]
    public class BackpackLua
    {
        public void SetNum(int slot, string id, int num)
        {
            BackpackManager.Instance.SetNum((BackpackSlot)slot, id, num);
        }
        public void AddNum(int slot, string id, int num)
        {
            BackpackManager.Instance.AddNum((BackpackSlot)slot, id, num);
        }
        public void GetNum(int slot, string id)
        {
            BackpackManager.Instance.GetNum((BackpackSlot)slot, id);
        }
        public string GetEquipped(int slot)
        {
            return BackpackManager.Instance.GetEquipped((EquipmentSlot)slot)?.equipmentID ?? "";
        }
        public void Equip(int slot, string equipID)
        {
            BackpackManager.Instance.Equip((EquipmentSlot)slot, equipID);
        }
    }
}