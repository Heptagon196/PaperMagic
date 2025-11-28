require('Lib/Equipment.lua')
---@type EquipmentInfo
local Data = {
    ID = 'std.advanced_wand',
    Name = '高级装备',
    Desc = '能装备在任何槽位上',
    Icon = 'Equip/std/default_wand.png',
    Capacity = 10,
    Slot = EquipmentSlot.All,
    CastType = EquipmentCastType.PressKey,
    MaxMana = 100,
    ManaResume = 100,
    CastInterval = 0.2,
}

function Data:OnAdd()
end

function Data:OnDel()
end

return Data