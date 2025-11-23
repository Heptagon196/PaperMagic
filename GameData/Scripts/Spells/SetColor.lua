require('Lib/SpellTool.lua')
---@type SpellInfo
local Data = SpellData:New{
	ID = 'std.set_color_red',
    Category = SpellType.Modifier,
	Name = '红色',
	Desc = '让法术变红',
    Icon = 'Spell/std/set_color_red.png',
	Cost = 10,
	ChildNode = 1,

    Color = CS.UnityEngine.Color(1, 0, 0, 1)
}

function Data:OnStart()
end

function Data:OnCast(EffectList, Spell)
    for i = 0, EffectList.Count - 1 do
        local effect = EffectList[i]
        -- print(Spell:GetEffectInSpellTreePos(Projectile))
		PatchSpellEffect(effect, 'OnApply', function(self, pos, dir)
			self.Owner:GetComponentInChildren(typeof(CS.UnityEngine.SpriteRenderer)).color = Data.Color
		end)
	end
	return self.Cost
end

return Data