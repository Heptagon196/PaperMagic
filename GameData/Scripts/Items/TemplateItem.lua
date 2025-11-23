require('Lib/BaseItem.lua')
---@type NormalItemInfo
local Data = {
    ID = 'std.default',
    Name = '治疗物品',
    Type = NormalItemType.Normal,
    Desc = '恢复30点生命',
    Icon = 'Item/std/default.png',

    Recovery = 30,
}

function Data:OnUseItem()
    PM.Creature:DoDamage(nil, PM.Player:GetObject(), -self.Recovery)
    return 1
end

return Data;