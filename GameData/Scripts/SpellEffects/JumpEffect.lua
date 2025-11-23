---@type SpellEffectInfo
local Data = {
    ID = 'std.jump',
    UpdateInterval = -1,
    ExpireTime = 0,

    JumpSpeed = 6,
}

function Data:OnApply(pos, towards)
    local vec = PM.Player:GetVelocity()
    vec.y = self.JumpSpeed
    PM.Player:SetVelocity(vec)
end

return Data