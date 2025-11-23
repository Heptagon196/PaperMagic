require('Lib/AI.lua')
local Data = BaseAI:New{
    ID = 'std.patrol',
    Name = '巡逻小兵',
    Faction = CreatureFaction.Hostile,
    Level = CreatureLevel.Normal,
    Health = 20,
    AnimationFolder = 'Creature/std/Patrol',
    Animations = {
        { anim = CreatureAnimStage.Death, duration = 1, num = 2 },
        { anim = CreatureAnimStage.Idle, duration = 1, num = 1 },
        { anim = CreatureAnimStage.Walk, duration = 0.1, num = 2 },
    },

    SpellID = 'std.default',
    Speed = 3,
    AttackInterval = 3,
    ChangeDirectionInterval = 5,
    ViewRange = 10,
    WalkAwayRange = 5,
    HateTime = 15,
    _lastAttackTime = 0,
    _lastChangeDirection = 0;
    _direction = 1,
    _leastDuration = 0,
}

function Data:OnStateEnter(state)
    if (state == AIState.Wandering) then
        self._leastDuration = 0
        self:SetAnim(CreatureAnimStage.Walk)
    elseif (state == AIState.Attack) then
        self._leastDuration = 0
        self:SetAnim(CreatureAnimStage.Idle)
    elseif (state == AIState.WalkAway) then
        self._leastDuration = 0.5
        self:SetAnim(CreatureAnimStage.Walk)
    elseif (state == AIState.WalkTowards) then
        self._leastDuration = 1
        self:SetAnim(CreatureAnimStage.Walk)
    end
end

function Data:OnStateExit(state)
end

function Data:TryAttack(func)
    if (self:GetTime() - self._lastAttackTime > self.AttackInterval) then
        self._lastAttackTime = self:GetTime()
        func()
    end
end

function Data:TryChangeDirection(func)
    if (self:GetTime() - self._lastChangeDirection > self.ChangeDirectionInterval) then
        self._lastChangeDirection = self:GetTime()
        func()
    end
end

function Data:OnStart()
    self.rigidbody = self.Owner:GetComponent(typeof(CS.UnityEngine.Rigidbody))
    self.rigidbody.velocity = CS.UnityEngine.Vector3(self._direction * self.Speed, 0, 0)
    self:ChangeState(AIState.Wandering)
end

function Data:UpdateStateMachine(deltaTime)
    if (self.currentState == AIState.Wandering) then
        self:TryChangeDirection(function()
            self._direction = -self._direction
            self.rigidbody.velocity = CS.UnityEngine.Vector3(self._direction * self.Speed, 0, 0)
        end)
    elseif (self.currentState == AIState.WalkAway) then
        local playerPos = PM.Player:GetPosition()
        local pos = self.Owner.transform.position
        self.rigidbody.velocity = CS.UnityEngine.Vector3(self.Speed * 2 * (playerPos.x < pos.x and 1 or -1), 0, 0)
    elseif (self.currentState == AIState.WalkTowards) then
        local playerPos = PM.Player:GetPosition()
        local pos = self.Owner.transform.position
        self.rigidbody.velocity = CS.UnityEngine.Vector3(self.Speed * (playerPos.x > pos.x and 1 or -1), 0, 0)
    elseif (self.currentState == AIState.Attack) then
        self.rigidbody.velocity = CS.UnityEngine.Vector3.zero
        self:TryAttack(function()
            local pos = self.Owner.transform.position
            PM.Creature:CastSpell(self.Owner, self.SpellID, pos, (PM.Player:GetPosition() - pos))
        end)
    end
    if (self:GetTime() - self.stateEnterTime < self._leastDuration) then
        return
    end
    if (PM.Creature:CanSeeTarget(self.Owner, PM.Player:GetObject(), self.WalkAwayRange)) then
        self:ChangeState(AIState.WalkAway)
    elseif (PM.Creature:CanSeeTarget(self.Owner, PM.Player:GetObject(), self.ViewRange)) then
        self:ChangeState(AIState.Attack)
    elseif (self:GetTime() - self._lastAttackTime < self.HateTime) then
        self:ChangeState(AIState.WalkTowards)
    else
        self:ChangeState(AIState.Wandering)
    end
end

return Data