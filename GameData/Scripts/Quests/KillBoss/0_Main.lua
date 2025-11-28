require('Lib/Quest.lua')
require('Lib/Backpack.lua')
local Data = QuestData:New{
    ID = 'std.side_kill_boss',
    Name = '击杀敌人',
    Desc = '击杀营地右边两个敌人',
    Notify = {
        QuestNotifyEvent.EnemyKill,
    },
    Type = QuestCategory.SideQuest,
    Require = {
        'std.init_quest_buy'
    },
    Level = 2,
    TargetCount = 2,
}

function Data:GetDesc()
    local status = PM.Quest:GetStatus(self.ID)
    if (status == QuestStatus.Completed) then
        return '任务完成，demo结束'
    end
    return string.format('%s [%d/%d]', self.Desc, self:GetFloat('count'), self.TargetCount)
end

function Data:OnActivate()
    PM.Quest:FocusQuest(self.ID)
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