require('Lib/Quest.lua')
local Data = QuestData:New{
    ID = 'std.default',
    Name = '模板任务',
    Desc = '击杀敌人',
    Notify = {
        QuestNotifyEvent.EnemyKill
    },
    Type = QuestCategory.MainQuest,
    SubQuests = {
        'std.default.kill10', -- 击杀10个敌人
        'std.default.kill1_special', -- 击杀1个精英
    },
}

function Data:OnActivate()
end

function Data:GetStatus(subCompleted, event, gameObject, objID)
    if (subCompleted) then
        return QuestStatus.Completed
    else
        return QuestStatus.NotCompleted
    end
end

return Data