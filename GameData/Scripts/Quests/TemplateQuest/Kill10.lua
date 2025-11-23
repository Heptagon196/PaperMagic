require('Lib/Quest.lua')
local Data = QuestData:New{
    ID = 'std.default.kill10',
    Desc = '击杀10个敌人',
    Type = QuestCategory.QuestGoal,

    Level = 0,
    TargetCount = 10,
}

function Data:OnActivate()
end

function Data:GetDesc()
    return string.format('%s [%d/%d]', self.Desc, self:GetFloat('count'), self.TargetCount)
end

function Data:GetStatus(subCompleted, event, gameObject, objID)
    if (event == QuestNotifyEvent.EnemyKill and PM.Creature:GetCreatureLevel(gameObject) == self.Level) then
        self:AddFloat('count', 1)
        if (self:GetFloat('count') >= self.TargetCount) then
            return QuestStatus.Completed
        end
    end
    return QuestStatus.NotCompleted
end

return Data