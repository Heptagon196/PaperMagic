require('Lib/Equipment.lua')
---@type EquipmentInfo
local Data = {
    ID = 'std.default',
    Name = '默认装备',
    Desc = '这是一件默认装备',
    Icon = 'Equip/std/default_wand.png',
    Slot = EquipmentSlot.Weapon,
    CastType = EquipmentCastType.PressKey,
    MaxMana = 100,
    ManaResume = 100,
    CastInterval = 0.1,
}

function Data:OnAdd()
end

function Data:OnDel()
end

return Data