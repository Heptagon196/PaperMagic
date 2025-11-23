using PMLua;
using XLua;

namespace Backpack
{
    public class NormalItemLua : NormalItemBase
    {
        private LuaTable _module;
        private OnUseItemDelegate _onUseItem;
        public NormalItemLua(LuaTable module)
        {
            if (module == null)
            {
                return;
            }
            _module = module;
            itemID = module.Get<string>("ID") ?? "";
            itemName = module.Get<string>("Name") ?? "";
            itemType = (NormalItemType)module.TryGet<int>("Type", 0);
            itemDesc = module.Get<string>("Desc") ?? "";
            itemIcon = module.Get<string>("Icon") ?? "";
            _onUseItem = module.Get<OnUseItemDelegate>("OnUseItem");
        }
        public override int UseItem()
        {
            return _onUseItem(_module);
        }
        public override NormalItemBase CopyNormalItem()
        {
            return new NormalItemLua(_module);
        }
    }
}