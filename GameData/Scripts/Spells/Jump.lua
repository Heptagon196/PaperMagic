require('Lib/SpellTool.lua')
---@type SpellInfo
local Data = SpellData:New{
	ID = 'std.jump',
    Category = SpellType.Other,
	Name = '跳跃',
	Desc = '让你跳起来',
    Icon = 'Spell/std/jump.png',
	Cost = 50,
	ChildNode = 0,

    JumpSpeed = 6,
}

function Data:OnStart()
end

function Data:OnCast(EffectList, Spell)
	local effect = PM.Effect:Spawn('std.jump')
	ToPatchable(effect).JumpSpeed = self.JumpSpeed
	EffectList:Add(effect)
	return self.Cost
end

return Data