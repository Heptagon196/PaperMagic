require('Lib/AI.lua')
local Enemy = BaseAI:New{
    ID = 'std.slime',
    Name = '史莱姆',
    Faction = CreatureFaction.Hostile,
    Level = CreatureLevel.Elite,
    Health = 15,
    AnimationFolder = 'Creature/std/Player_Yellow',
    Animations = {
        { anim = CreatureAnimStage.Death, duration = 1, num = 1 },
        { anim = CreatureAnimStage.Idle, duration = 1, num = 1 },
        { anim = CreatureAnimStage.Walk, duration = 0.1, num = 8 },
    },

    SpellID = 'std.default',
    Speed = 2,
    AttackBackSwing = 2,
    ChangeDirectionInterval = 3,
    ViewRange = 3,
    HateTime = 5,
    PatrolStayTime = 1,

    _lastFindPlayerPos = nil,
    _lastFindPlayerTime = nil,
    _lastChangeDirection = 0;
    _patrolStayStart = nil,
    _direction = 1,
}

function Enemy:UpdateStateMachine(deltaTime)
    self.tree:Update()
end

function Enemy:OnStart()
    self.rigidbody = self.Owner:GetComponent(typeof(CS.UnityEngine.Rigidbody))
    self.rigidbody.velocity = CS.UnityEngine.Vector3(self._direction * self.Speed, 0, 0)
    local spriteObj = self.Owner:GetComponentInChildren(typeof(CS.UnityEngine.SpriteRenderer))
    spriteObj.transform.localPosition = CS.UnityEngine.Vector3(0, 0.3, 0)
    self:SetAnim(CreatureAnimStage.Idle)

    local root = SelectorNode:New('Root')
    self.tree = BehaviorTree:New(root)
    SequenceNode:New('Attack'):AddTo(root)
        :Add(ConditionNode:New('CanAttack', function()
            return PM.Creature:CanSeeTarget(self.Owner, PM.Player:GetObject(), self.ViewRange)
        end))
        :Add(AsyncNode:New('Attack', function()
            self._lastFindPlayerPos = PM.Player:GetPosition()
            self._lastFindPlayerTime = self:GetTime()
            self.rigidbody.velocity = CS.UnityEngine.Vector3.zero
            local pos = self.Owner.transform.position
            PM.Creature:CastSpell(self.Owner, self.SpellID, pos, (PM.Player:GetPosition() - pos))
            self:SetAnim(CreatureAnimStage.Idle)
            AsyncWaitSeconds(self.AttackBackSwing)
            coroutine.yield(BTState.Success)
        end))
    SequenceNode:New('Search'):AddTo(root)
        :Add(ConditionNode:New('HasPos', function()
            if (self._lastFindPlayerTime == nil) then
                return false
            end
            if (self:GetTime() - self._lastFindPlayerTime > self.HateTime) then
                self._lastFindPlayerPos = nil
            end
            return self._lastFindPlayerPos ~= nil
        end))
        :Add(ActionNode:New('WalkClose', function()
            local playerPos = PM.Player:GetPosition()
            local pos = self.Owner.transform.position
            self.rigidbody.velocity = CS.UnityEngine.Vector3(self.Speed * (playerPos.x > pos.x and 1 or -1), 0, 0)
            self:SetAnim(CreatureAnimStage.Walk)
            return BTState.Success
        end))
    ActionNode:New('Patrol', function()
        self:SetAnim(CreatureAnimStage.Walk)
        self:TryChangeDirection(function(dir)
            self.rigidbody.velocity = CS.UnityEngine.Vector3(dir * self.Speed, 0, 0)
        end)
        return BTState.Success
    end):AddTo(root)
end

function Enemy:TryChangeDirection(func)
    if (self._patrolStayStart ~= nil) then
        if (self:GetTime() - self._patrolStayStart > self.PatrolStayTime) then
            self._lastChangeDirection = self:GetTime()
            self._direction = -self._direction
            self._patrolStayStart = nil
            func(self._direction)
            return true
        end
        return false
    end
    if (self:GetTime() - self._lastChangeDirection > self.ChangeDirectionInterval) then
        self._patrolStayStart = self:GetTime()
        func(0)
        return true
    end
    return false
end

return Enemy