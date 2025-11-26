require('Lib/Quest.lua')
local Data = QuestData:New{
    ID = 'std.init_quest',
    Name = '教学任务',
    Desc = '学会基本操作',
    Notify = {
        QuestNotifyEvent.BackpackChanged,
        QuestNotifyEvent.EnemyKill,
    },
    Type = QuestCategory.MainQuest,
    SubQuests = {
        'std.init_quest.equip_wand',
        'std.init_quest.equip_spell',
        'std.init_quest.kill_enemy',
    },
}

function Data:OnActivate()
end

return Data