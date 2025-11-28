require('Lib/BaseItem.lua')
---@type NormalItemInfo
local Data = {
    ID = 'std.heal_item',
    Name = '治疗物品',
    Type = NormalItemType.Normal,
    Desc = '<size=20>治疗道具</size>\n\n<i>恢复<color=Green>30</color>点生命</i>',
    Icon = 'Item/std/default.png',

    Recovery = 30,
}

function Data:OnUseItem()
    PM.Creature:DoDamage(nil, PM.Player:GetObject(), -self.Recovery)
    return 1
end

return Data;