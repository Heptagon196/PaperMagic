---@enum NormalItemType
NormalItemType = {
    Normal = 0,
    Quest = 1,
    Usable = 2,
}

---@class NormalItemInfo
---@field ID string 标识符
---@field Name string 名称
---@field Type NormalItemType 类型
---@field Desc string 描述
---@field Icon string 图标
---@field OnUseItem? fun(self: table): integer