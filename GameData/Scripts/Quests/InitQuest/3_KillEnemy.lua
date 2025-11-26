require('Lib/Quest.lua')
require('Lib/Equipment.lua')
local Data = QuestData:New{
    ID = 'std.init_quest.kill_enemy',
    Desc = '使用法术击败敌人。[%d/%d]\n法杖的触发方式为：\n头部：按Q\n衣服：按E\n左手：鼠标左键\n右手：鼠标右键\n鞋子：空格',
    Type = QuestCategory.QuestGoal,

    CompleteDesc = '已击败敌人。\n法杖的法力会随时间自动恢复。\n向前走吧。',
    PrevQuest = 'std.init_quest.equip_spell',
    TargetCount = 3,
}

function Data:GetDesc()
    local status = PM.Quest:GetStatus(self.ID)
    if (status == QuestStatus.Completed) then
        return self.CompleteDesc
    else
        return string.format(self.Desc, self:GetFloat('count'), self.TargetCount)
    end
end

function Data:GetStatus(subCompleted, event, gameObject, objID)
    if (PM.Quest:GetStatus(self.PrevQuest) ~= QuestStatus.Completed) then
        return QuestStatus.Hide
    end
    if (not self:GetBool('summoned')) then
        self:SetBool('summoned', true)
        local spawn = PM.Creature:Spawn('std.slime', PM.Player:GetPosition() + CS.UnityEngine.Vector3(2, 0, 0))
        PM.Creature:SetPersistent(spawn)
        PM.Creature:SetPersistent(PM.Creature:Spawn('std.slime', PM.Player:GetPosition() + CS.UnityEngine.Vector3(4, 0, 0)))
        PM.Creature:SetPersistent(PM.Creature:Spawn('std.elite_patrol', PM.Player:GetPosition() + CS.UnityEngine.Vector3(6, 0, 0)))
    end
    if (event == QuestNotifyEvent.EnemyKill) then
        self:AddFloat('count', 1)
        if (self:GetFloat('count') >= self.TargetCount) then
            return QuestStatus.Completed
        end
    end
    return QuestStatus.NotCompleted
end

return Data