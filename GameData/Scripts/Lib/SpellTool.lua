---@enum SpellType
SpellType = {
    Projectile = 0, -- 投射物
    Modifier = 1, -- 修饰
    Other = 2, -- 其它
}

---@class SpellInfo
---@field ID string
---@field Category SpellType
---@field Name string
---@field Desc string
---@field Icon string
---@field Cost number
---@field Damage? number
---@field ChildNode integer
---@field OnStart? fun(self: table): nil
---@field OnCast? fun(self: table, effectList: any, spellTree: any): number

local MetaTable = {
    __newindex = function(table, key, value)
        local valType = type(value)
        if (valType == 'number') then
            table.__effect:SetFloat(key, value)
        elseif (valType == 'string') then
            table.__effect:SetString(key, value)
        else
            table.__effect:SetObject(key, value)
        end
    end,
    __index = function(table, key)
        if (table.__effect:ContainsData(key)) then
            return table.__effect:GetObject(key)
        end
        return table.__effect[key]
    end
}

---@param func fun(self: SpellEffectInfo, ...) : nil
function PatchSpellEffect(effect, event, func)
    effect[event]:Add(function(self, ...)
        local data = {}
        data.__effect = self
        func(setmetatable(data, MetaTable), ...)
    end)
end

---@return SpellEffectInfo
function ToPatchable(effect)
    local data = {}
    data.__effect = effect
    return setmetatable(data, MetaTable)
end

function GetRigidbody(gameObject)
    return gameObject:GetComponent(typeof(CS.UnityEngine.Rigidbody))
end

---@class SpellInfoExtended: SpellInfo
SpellData = {}
---@param table SpellInfo
---@return SpellInfoExtended
function SpellData:New(table)
    setmetatable(table, self)
    self.__index = self
    ---@diagnostic disable-next-line: return-type-mismatch
    return table
end