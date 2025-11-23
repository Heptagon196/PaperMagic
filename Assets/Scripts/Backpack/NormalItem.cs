using System;
using XLua;

namespace Backpack
{
    [CSharpCallLua]
    public delegate int OnUseItemDelegate(LuaTable self);
    [Serializable]
    public enum NormalItemType
    {
        Normal,
        Quest,
        Usable,
    }
    [Serializable]
    public class NormalItemBase
    {
        // 装备标识符
        public string itemID;
        // 名称
        public string itemName;
        // 类型
        public NormalItemType itemType;
        // 描述
        public string itemDesc;
        // 图标
        public string itemIcon;
        // 使用物品 返回值表示消耗物品数量
        public virtual int UseItem()
        {
            return 0;
        }
        public virtual NormalItemBase CopyNormalItem()
        {
            return new NormalItemBase()
            {
                itemID = itemID,
                itemName = itemName,
                itemType = itemType,
                itemDesc = itemDesc,
                itemIcon = itemIcon,
            };
        }
    }
    public class NormalItemEmpty : NormalItemBase
    {
        public NormalItemEmpty()
        {
            itemID = NormalItemManager.EmptyItem;
            itemName = NormalItemManager.EmptyItem;
            itemType = NormalItemType.Normal;
            itemDesc = NormalItemManager.EmptyItem;
            itemIcon = "add_sign.png";
        }
    }
}