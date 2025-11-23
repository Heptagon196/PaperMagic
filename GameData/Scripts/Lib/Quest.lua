---@enum QuestCategory
QuestCategory = {
    MainQuest = 0,
    SideQuest = 1,
    QuestGoal = 2,
}

---@enum QuestNotifyEvent
QuestNotifyEvent = {
    None = 0,
    EnemyKill = 1,
    QuestChatFinish = 2,
    BackpackChanged = 3,
}

---@enum QuestStatus
QuestStatus = {
    None = 0,
    Hide = 1,
    NotCompleted = 2,
    Completed = 3,
    Failed = 4,
}

---@class QuestInfo
---@field ID string 标识符
---@field Name? string 名称
---@field Desc string 描述
---@field Type QuestCategory 类型
---@field Notify? QuestNotifyEvent[] 发生哪些事件时检测任务状态
---@field Optional? boolean 是否是可选任务
---@field SubQuests? string[] 子任务
---@field Require? string[] 前置任务，全部完成后自动接取此任务
---@field Sort? integer 排序
---@field GetStatus? fun(self: table, subQuestsCompleted: boolean, event: QuestNotifyEvent, obj: any, objID: string): QuestStatus 获取任务状态
---@field OnActivate? fun(self: table): nil 接取任务时触发
---@field GetDesc? fun(self: table): string 不设置或返回nil时使用desc，否则使用返回值覆盖描述

---@class QuestInfoExtended: QuestInfo
QuestData = {}
---@param table QuestInfo
---@return QuestInfoExtended
function QuestData:New(table)
    setmetatable(table, self)
    self.__index = self
    ---@diagnostic disable-next-line: return-type-mismatch
    return table
end

function QuestData:GetBool(key)
    return PM.Quest:GetBool(self.ID, key)
end

function QuestData:SetBool(key, value)
    PM.Quest:SetBool(self.ID, key, value)
end

function QuestData:GetFloat(key)
    return PM.Quest:GetFloat(self.ID, key)
end

function QuestData:SetFloat(key, value)
    PM.Quest:SetFloat(self.ID, key, value)
end

function QuestData:AddFloat(key, value)
    self:SetFloat(key, self:GetFloat(key) + value)
end

function QuestData:GetString(key)
    return PM.Quest:GetString(self.ID, key)
end

function QuestData:SetString(key, value)
    PM.Quest:SetString(self.ID, key, value)
end