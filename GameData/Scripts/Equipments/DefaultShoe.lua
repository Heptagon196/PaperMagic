require('Lib/Equipment.lua')
---@type EquipmentInfo
local Data = {
    ID = 'std.default_shoe',
    Name = '默认鞋子',
    Desc = '这是默认鞋子',
    Icon = 'Equip/std/default_shoe.png',
    Capacity = 1,
    Slot = EquipmentSlot.Foot,
    CastType = EquipmentCastType.PressKey,
    MaxMana = 100,
    ManaResume = 50,
    CastInterval = 0.5,
}

function Data:OnAdd()
end

function Data:OnDel()
end

return Data