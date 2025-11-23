require('Lib/SpellTool.lua')
---@type SpellInfoExtended
local Data = SpellData:New{
    ID = 'std.double_cast',
    Category = SpellType.Modifier,
	Name = '双重释放',
	Desc = '释放两个法术，增加散射',
    Icon = 'Spell/std/double_cast.png',
    Cost = 10,
	ChildNode = 2,

    SplitAngle = 20,
}

function Data:OnStart()
end

function Data:OnCast(EffectList, Spell)
    for i = 0, EffectList.Count - 1 do
        local effect = EffectList[i]
        -- local Order = Spell:GetEffectInSpellTreePos(Projectile)
        PatchSpellEffect(effect, 'OnApply', function(self, pos, dir)
            local rigidbody = GetRigidbody(self.Owner)
            local angle = Data.SplitAngle * (math.random() - 0.5)
            rigidbody.velocity = PM:RotateVec(rigidbody.velocity, angle)
        end)
    end
	return self.Cost
end

return Data
