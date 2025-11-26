require('Lib/Quest.lua')
require('Lib/Equipment.lua')
local Data = QuestData:New{
    ID = 'std.init_quest.equip_wand',
    Desc = '在左手装备法杖',
    Type = QuestCategory.QuestGoal,

    CompleteDesc = '已装备法杖',
}

function Data:GetDesc()
    local status = PM.Quest:GetStatus(self.ID)
    if (status == QuestStatus.Completed) then
        return self.CompleteDesc
    else
        return self.Desc
    end
end

function Data:GetStatus(subCompleted, event, gameObject, objID)
    if (event == QuestNotifyEvent.BackpackChanged) then
        local wand = PM.Backpack:GetEquipped(EquipmentSlot.WeaponLeft)
        if (wand == 'std.default') then
            return QuestStatus.Completed
        else
            return QuestStatus.NotCompleted
        end
    end
    return QuestStatus.NotCompleted
end

return Data