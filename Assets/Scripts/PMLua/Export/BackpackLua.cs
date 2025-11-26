using Backpack;
using Controller;
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
        public int GetNum(int slot, string id)
        {
            return BackpackManager.Instance.GetNum((BackpackSlot)slot, id);
        }
        public string GetEquipped(int slot)
        {
            return BackpackManager.Instance.GetEquipped((EquipmentSlot)slot)?.equipmentID ?? "";
        }
        public void Equip(int slot, string equipID)
        {
            BackpackManager.Instance.Equip((EquipmentSlot)slot, equipID);
        }
        public int SpellEquippedCount(int slot, string spellID)
        {
            return PlayerMagicController.Instance.SpellEquippedCount(slot, spellID);
        }
    }
}