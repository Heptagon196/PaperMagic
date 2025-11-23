require('Lib/SpellTool.lua')
---@type SpellInfo
local Data = SpellData:New{
	ID = 'std.trigger_on_expired',
    Category = SpellType.Modifier,
	Name = '消逝触发',
	Desc = '第一个子节点法术消失时\n触发第二个子节点\n触发法术沿当前方向前进',
    Icon = 'Spell/std/set_color_red.png',
	Cost = 10,
	ChildNode = 2,
}

function Data:OnStart()
end

function Data:OnCast(EffectList, Spell)
	local CachedEffectList = {}
	if (EffectList.Count < 2) then
		return self.Cost
	end
    for i = 0, EffectList.Count - 1 do
        local effect = EffectList[i]
        local nodePos = Spell:GetEffectInSpellTreePos(effect)
		if (nodePos == 0) then
			ToPatchable(effect).ExpireTime = 1
			PatchSpellEffect(effect, 'OnExpired', function(self)
				for _, spawnEffect in pairs(CachedEffectList) do
					local rigidbody = GetRigidbody(self.Owner)
					PM.Effect:Instantiate(spawnEffect, self.Owner.transform.position, rigidbody.velocity.normalized, self.Source)
				end
			end)
		else
			table.insert(CachedEffectList, effect)
			EffectList:Remove(effect)
		end
	end
	return self.Cost
end

return Data