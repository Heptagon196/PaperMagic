require('Lib/AI.lua')
local Data = BaseAI:New{
    ID = 'std.merchant_base',
    Name = '商人模板',
    Faction = CreatureFaction.Friendly,
    Level = CreatureLevel.Normal,
    Health = 100,
    AnimationFolder = 'Creature/std/Player_Green',
    Animations = {
        { anim = CreatureAnimStage.Idle, duration = 1, num = 1 },
    },

    InteractRange = 2.5,
    InteractScript = 'Text/TemplateChat.lua',
    canInteract = false,
}

function Data:OnStart()
    self:SetAnim(CreatureAnimStage.Idle)
    self.Owner:GetComponent(typeof(CS.UnityEngine.Rigidbody)).isKinematic = true
    self.Owner:GetComponent(typeof(CS.UnityEngine.CapsuleCollider)).isTrigger = true
    local spriteObj = self.Owner:GetComponentInChildren(typeof(CS.UnityEngine.SpriteRenderer))
    spriteObj.transform.localPosition = CS.UnityEngine.Vector3(0, 0.3, 0)
end

function Data:OnDeath()
    PM.Chat:TryCloseChat(self.InteractScript)
end

function Data:OnInteract()
	PM.Chat:StartChat(self.InteractScript)
end

function Data:UpdateStateMachine()
    if (PM.Creature:CanSeeTarget(self.Owner, PM.Player:GetObject(), self.InteractRange)) then
        if (self.canInteract == false) then
            self.canInteract = true
            PM.Chat:SetInteractable(self.XID, true, function()
                self:OnInteract()
            end)
        end
    else
        if (self.canInteract == true) then
            self.canInteract = false
            PM.Chat:SetInteractable(self.XID, false, nil)
        end
    end
end

return Data