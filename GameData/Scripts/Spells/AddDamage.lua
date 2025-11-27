require('Lib/SpellTool.lua')
---@type SpellInfoExtended
local Data = SpellData:New{
    ID = 'std.add_damage',
    Category = SpellType.Modifier,
	Name = '增加伤害',
	Desc = '为子节点法术增加伤害',
    Icon = 'Spell/std/add_damage.png',
	Damage = 7,
    Cost = 10,
	ChildNode = 1,
}

function Data:OnStart()
end

function Data:OnCast(EffectList, Spell)
    for i = 0, EffectList.Count - 1 do
        local effect = ToPatchable(EffectList[i])
        effect.Damage = effect.Damage + self.Damage
    end
	return self.Cost
end

return Data
