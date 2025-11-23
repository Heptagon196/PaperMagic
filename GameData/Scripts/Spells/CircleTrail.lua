require('Lib/SpellTool.lua')
---@type SpellInfoExtended
local Data = SpellData:New{
    ID = 'std.circle_trail',
    Category = SpellType.Modifier,
	Name = '圆形轨迹',
	Desc = '让法术顺时针旋转\n延长法术持续时间',
    Icon = 'Spell/std/circle_trail.png',
    Cost = 10,
	ChildNode = 1,
}

function Data:OnStart()
end

local function TurnDirection(vec)
    return CS.UnityEngine.Vector3(-vec.y, vec.x, vec.z).normalized
end

function Data:OnCast(EffectList, Spell)
    for i = 0, EffectList.Count - 1 do
        local effect = EffectList[i]
        -- print(Spell:GetEffectInSpellTreePos(Projectile))
        PatchSpellEffect(effect, 'OnApply', function(self, pos, dir)
            -- 延长持续时间
            self.ExpireTime = self.ExpireTime * 2
            self.rigidbody = GetRigidbody(self.Owner)
            -- 旋转中心
            self.CirclePoint = TurnDirection(self.rigidbody.velocity) * self.CircleRadius + pos
        end)
        PatchSpellEffect(effect, 'OnUpdate', function(self, deltaTime)
            -- 转向
            self.rigidbody.velocity =
                TurnDirection(self.Owner.transform.position - self.CirclePoint) * self.ShootSpeed
        end)
    end
	return self.Cost
end

return Data
