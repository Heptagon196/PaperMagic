require('Lib/Quest.lua')
local Data = QuestData:New{
    ID = 'std.default2',
    Name = '模板任务2',
    Desc = '击杀敌人2',
    Notify = {},
    Require = {
        "std.default"
    },
    Type = QuestCategory.MainQuest,
}

function Data:OnActivate()
end

function Data:GetStatus(subCompleted, event, gameObject, objID)
    return QuestStatus.NotCompleted
end

return Data