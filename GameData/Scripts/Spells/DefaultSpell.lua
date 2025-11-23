require('Lib/SpellTool.lua')
---@type SpellInfo
local Data = SpellData:New{
	ID = 'std.default',
    Category = SpellType.Projectile,
	Name = '圆形子弹',
	Desc = '生成一个圆形子弹',
    Icon = 'Spell/std/default.png',
	Damage = 10,
	Cost = 10,
	ChildNode = 0,
}

function Data:OnStart()
end

function Data:OnCast(EffectList)
	local effect = PM.Effect:Spawn('std.default_projectile')
	ToPatchable(effect).Damage = self.Damage
	EffectList:Add(effect)
	return self.Cost
end

return Data
