require('Lib/Quest.lua')
require('Lib/Backpack.lua')
local Data = QuestData:New{
    ID = 'std.init_quest',
    Name = '教学任务',
    Desc = '学会基本操作',
    Notify = {
        QuestNotifyEvent.BackpackChanged,
        QuestNotifyEvent.EnemyKill,
        QuestNotifyEvent.CloseBackpack,
    },
    Type = QuestCategory.MainQuest,
    SubQuests = {
        'std.init_quest.equip_wand',
        'std.init_quest.equip_spell',
        'std.init_quest.kill_enemy',
    },
}

function Data:OnActivate()
    PM.Quest:FocusQuest(self.ID)
    PM.Backpack:AddNum(BackpackSlot.Equipment, 'std.default', 1)
    PM.Backpack:AddNum(BackpackSlot.Equipment, 'std.default_shoe', 1)
end

return Data