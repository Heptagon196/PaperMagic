---@type SpellEffectInfo
local Data = {
    ID = 'std.default_projectile',
    UpdateInterval = 0.1,
    ExpireTime = 3,
    Damage = 5,

    ShootSpeed = 15,
    CircleRadius = 3,
    Color = CS.UnityEngine.Color(1, 1, 1, 1),
    StartTime = 0,
}

function Data:OnApply(pos, towards)
    self.Owner.transform.position = pos
    self.Owner:GetComponent(typeof(CS.UnityEngine.Rigidbody)).velocity = self.ShootSpeed * towards
	self.Owner:GetComponentInChildren(typeof(CS.UnityEngine.SpriteRenderer)).color = self.Color
    self.StartTime = CS.UnityEngine.Time.time
end

function Data:OnUpdate()
end

function Data:OnTriggerEnter(other)
    if (not PM:IsValid(self.Source)) then
        return
    end
    -- 不攻击自身
    if (self.Source.gameObject == other.gameObject) then
        return
    end
    if (other.transform:CompareTag('Bullet')) then
        -- 不与玩家自身子弹碰撞
        local effect = PM.Projectile:GetProjectileEffect(other.gameObject)
        if (effect ~= nil and effect:GetObject('Source') == self.Source) then
            return
        end
    end
    PM.Projectile:TryDoDamage(self.Owner, other.gameObject, self.Damage)
    PM.Projectile:Destroy(self.Owner)
end

function Data:OnExpired()
end

return Data