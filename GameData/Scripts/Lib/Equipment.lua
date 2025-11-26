---@enum EquipmentSlot
EquipmentSlot = {
    Hat = 1 << 1,
    Coat = 1 << 2,
    WeaponLeft = 1 << 3,
    WeaponRight = 1 << 4,
    Foot = 1 << 5,

    Weapon = (1 << 3) | (1 << 4),
    All = (1 << 6) - 1,
    None = 0,
}

---@enum EquipmentCastType
EquipmentCastType = {
    Automatic = 0,
    Passive = 1,
    PressKey = 2,
}

---@class EquipmentInfo
---@field ID string 标识符
---@field Name string 名称
---@field Desc string 描述
---@field Icon string 图标
---@field Capacity integer 容量
---@field Slot EquipmentSlot 槽位
---@field CastType EquipmentCastType 施法方式
---@field MaxMana number 最大法力值
---@field ManaResume number 每秒回复法力值
---@field CastInterval number 施法间隔
---@field OnAdd? fun(self: table): nil 穿戴时
---@field OnDel? fun(self: table): nil 脱下时