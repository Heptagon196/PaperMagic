require('Lib/Quest.lua')
require('Lib/Backpack.lua')
local Data = QuestData:New{
    ID = 'std.init_quest_buy',
    Name = '进入营地',
    Desc = '进入营地，向商人购买道具',
    Notify = {
        QuestNotifyEvent.BackpackChanged,
    },
    Type = QuestCategory.MainQuest,
    Require = {
        'std.init_quest'
    },
}

function Data:OnActivate()
    PM.Backpack:AddNum(BackpackSlot.Item, 'std.coin', 1000)
    self:SetFloat('coin', PM.Backpack:GetNum(BackpackSlot.Item, 'std.coin'))
end

function Data:GetStatus(subCompleted, event, gameObject, objID)
    if (event == QuestNotifyEvent.BackpackChanged) then
        local prevCoin = self:GetFloat('coin')
        local curCoin = PM.Backpack:GetNum(BackpackSlot.Item, 'std.coin')
        print(prevCoin, curCoin)
        self:SetFloat('coin', curCoin)
        if (curCoin < prevCoin) then
            return QuestStatus.Completed
        end
    end
    return QuestStatus.NotCompleted
end

return Data