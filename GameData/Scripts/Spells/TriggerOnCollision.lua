require('Lib/SpellTool.lua')
---@type SpellInfo
local Data = SpellData:New{
	ID = 'std.trigger_on_collision',
    Category = SpellType.Modifier,
	Name = '碰撞触发',
	Desc = '第一个子节点法术击中物体时\n触发第二个子节点\n触发法术随机方向发射',
    Icon = 'Spell/std/trigger_collision.png',
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
			PatchSpellEffect(effect, 'OnTriggerEnter', function(self, other)
				if (other.transform:CompareTag('Bullet')) then
					return
				end
				local source = self.Source
				if (other.gameObject == source.gameObject) then
					return
				end
				if (self.Triggered) then
					return
				end
				self.Triggered = true
				for _, spawnEffect in pairs(CachedEffectList) do
					local patched = ToPatchable(spawnEffect)
					if (not patched.Triggered) then
						patched.Triggered = true
						local rigidbody = GetRigidbody(self.Owner)
						local angle = math.random() * 2 * math.pi
						local towards = CS.UnityEngine.Vector3(math.cos(angle), math.sin(angle), 0)
						local pos = self.Owner.transform.position
						pos = pos + towards
						PM.Effect:Instantiate(spawnEffect, pos, towards, source)
					end
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