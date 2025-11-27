require('Lib/Quest.lua')
require('Lib/Equipment.lua')
local Data = QuestData:New{
    ID = 'std.init_quest.equip_spell',
    Desc = '在左手法杖上装备攻击法术，完成后关闭背包',
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
    if (self:GetBool('add_spell') == false) then
        self:SetBool('add_spell', 1)
        PM.Backpack:AddNum(BackpackSlot.Spell, 'std.default', 5)
        PM.Backpack:AddNum(BackpackSlot.Spell, 'std.set_color_red', 1)
        PM.Backpack:AddNum(BackpackSlot.Spell, 'std.double_cast', 2)
        PM.Backpack:AddNum(BackpackSlot.Spell, 'std.circle_trail', 1)
        PM:FloatMsg('获得了一批新的法术')
    end
    if (event == QuestNotifyEvent.CloseBackpack) then
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