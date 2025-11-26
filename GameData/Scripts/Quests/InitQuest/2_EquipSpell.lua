require('Lib/Quest.lua')
require('Lib/Equipment.lua')
local Data = QuestData:New{
    ID = 'std.init_quest.equip_spell',
    Desc = '在左手法杖上装备攻击法术',
    Type = QuestCategory.QuestGoal,

    CompleteDesc = '已装备法术',
    PrevQuest = 'std.init_quest.equip_wand',
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
    if (PM.Quest:GetStatus(self.PrevQuest) ~= QuestStatus.Completed) then
        return QuestStatus.Hide
    end
    if (event == QuestNotifyEvent.BackpackChanged) then
        local wand = PM.Backpack:GetEquipped(EquipmentSlot.WeaponLeft)
        if (wand == 'std.default') then
            if (PM.Backpack:SpellEquippedCount(EquipmentSlot.WeaponLeft, 'std.default') > 0) then
                return QuestStatus.Completed
            end
        end
    end
    return QuestStatus.NotCompleted
end

return Data