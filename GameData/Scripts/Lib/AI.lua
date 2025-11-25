---@class CreatureInfo
---@field ID string 生物种类标识符
---@field Name string 生物种类名称
---@field Faction CreatureFaction 阵营
---@field Level CreatureLevel 等阶
---@field Health number 生命上限
---@field Width? number 生物宽度
---@field Height? number 生物高度
---@field XID? number 生物实例唯一标识符
---@field AnimationFolder string
---@field Animations CreatureAnimGroup[]
---@field OnStart? fun(self: table): nil
---@field OnUpdate? fun(self: table): nil
---@field OnDeath? fun(self: table): nil
---@field OnStateEnter? fun(self: table, state: AIState): nil
---@field OnStateExit? fun(self: table, state: AIState): nil
---@field UpdateStateMachine? fun(self: table, deltaTime: number): nil

---@class CreatureAnimGroup
---@field anim CreatureAnimStage
---@field num integer
---@field duration number

---@enum AIState
AIState = {
    Idle = 0, -- 不移动
    RandomMove = 1, -- 随机移动
    Wandering = 2, -- 游荡
    JumpTowards = 3, -- 跳跃接近
    WalkTowards = 4, -- 移动接近
    WalkAway = 5, -- 远离
    Attack = 6, -- 攻击 
}

---@enum CreatureFaction
CreatureFaction = {
    Friendly = 0,
    Hostile = 1,
    Neutral = 2,
}

---@enum CreatureLevel
CreatureLevel = {
    Normal = 0,
    Elite = 1,
    Boss = 2,
}

---@enum CreatureAnimStage
CreatureAnimStage = {
    None = '',
    Death = 'death',
    Idle = 'idle',
    Walk = 'walk',
    Run = 'run',
    Jump = 'jump',
    Attack = 'attack',
    Shield = 'shield',
}

-- 基础AI类
---@class CreatureInfoExtended
BaseAI = {
    Owner = nil,
    isGrounded = false,
    ---@type AIState
    currentState = AIState.Idle,
    stateEnterTime = 0,
    XID = nil,
}

function BaseAI:OnInit()
    print("AI Initialized for: " .. self.Owner.name)
end

---@param anim CreatureAnimStage
function BaseAI:SetAnim(anim)
    PM.Creature:SetAnimation(self.Owner, anim)
end

function BaseAI:IsGrounded()
    return self.isGrounded
end

function BaseAI:GetTime()
    return CS.UnityEngine.Time.time
end

function BaseAI:GetDeltaTime()
    return CS.UnityEngine.Time.deltaTime
end

function BaseAI:OnUpdate()
    local deltaTime = self:GetDeltaTime()
    self:UpdateStateMachine(deltaTime)
end

---@param newState AIState
function BaseAI:ChangeState(newState)
    if self.currentState ~= newState then
        self:OnStateExit(self.currentState)
        self.currentState = newState
        self.stateEnterTime = self:GetTime()
        self:OnStateEnter(newState)
    end
end

-- function BaseAI:OnStateEnter(state) end
-- function BaseAI:OnStateExit(state) end
-- function BaseAI:UpdateStateMachine(deltaTime) end

---@param table CreatureInfo
---@return CreatureInfoExtended
function BaseAI:New(table)
    setmetatable(table, self)
    self.__index = self
---@diagnostic disable-next-line: return-type-mismatch
    return table
end

-- 行为树

---@enum BTState
BTState = {
    Success = 0,
    Failure = 1,
    Running = 2,
}

---@class BehaviorTree
BehaviorTree = {
    root = nil,
    currentNode = nil
}

function BehaviorTree:New(rootNode)
    local tree = {
        root = rootNode,
        currentNode = nil,
        _isRunning = false
    }
    setmetatable(tree, self)
    self.__index = self
    return tree
end

function BehaviorTree:Update()
    if (not self._isRunning) then
        self.currentNode = self.root
        self._isRunning = true
    end
    if (self.currentNode) then
        local result, ret = self.currentNode:Execute()
        if (result ~= BTState.Running) then
            self.currentNode:OnExit()
            self._isRunning = false
        else
            self.currentNode = ret
        end
    end
end

---@class BTNode
BTNode = {}

---@return BTNode
function BTNode:New(name)
    local node = {
        name = name or 'unnamed',
        children = {}
    }
    setmetatable(node, self)
    self.__index = self
    return node
end

---@return BTNode
function BTNode:Add(child)
    table.insert(self.children, child)
    return self
end

function BTNode:Execute()
    return BTState.Success
end

function BTNode:OnEnter() end
function BTNode:OnExit() end

---@param node BTNode
---@return BTNode
function BTNode:AddTo(node)
    node:Add(self)
    return self
end

-- 序列节点
---@class SequenceNode: BTNode
SequenceNode = BTNode:New('Sequence')

function SequenceNode:Execute()
    for _, child in ipairs(self.children) do
        local result, ret = child:Execute()
        if (result == BTState.Failure) then
            return BTState.Failure
        elseif (result == BTState.Running) then
            return BTState.Running, ret
        end
    end
    return BTState.Success
end

-- 选择节点
---@class SelectorNode: BTNode
SelectorNode = BTNode:New('Selector')

function SelectorNode:Execute()
    for _, child in ipairs(self.children) do
        local result, ret = child:Execute()
        if (result == BTState.Success) then
            return BTState.Success
        elseif (result == BTState.Running) then
            return BTState.Running, ret
        end
    end
    return BTState.Failure
end

-- 条件节点
---@class ConditionNode: BTNode
ConditionNode = BTNode:New('Condition')

function ConditionNode:New(name, conditionFunc)
    local node = BTNode.New(self, name)
    node.conditionFunc = conditionFunc
    return node
end

function ConditionNode:Execute()
    return self.conditionFunc() and BTState.Success or BTState.Failure
end

-- 动作节点
---@class ActionNode: BTNode
ActionNode = BTNode:New('Action')

function ActionNode:New(name, actionFunc)
    local node = BTNode.New(self, name)
    node.actionFunc = actionFunc
    return node
end

function ActionNode:Execute()
    return self.actionFunc(), self
end

-- 协程动作节点
---@class AsyncNode: BTNode
AsyncNode = BTNode:New('Async')

function AsyncNode:New(name, actionFunc)
    local node = BTNode.New(self, name)
    node.origFunc = actionFunc
    node.actionFunc = coroutine.create(actionFunc)
    return node
end

function AsyncNode:Execute()
    local success, result = coroutine.resume(self.actionFunc)
    local status = coroutine.status(self.actionFunc)
    local retStatus = BTState.Running
    if (status == 'dead') then
        retStatus = result or BTState.Success
    elseif (result) then
        retStatus = result
    end
    if (retStatus ~= BTState.Running) then
        self.actionFunc = coroutine.create(self.origFunc)
    end
    return retStatus, self
end

function AsyncWaitSeconds(seconds)
    local time = 0
    while (true) do
        if (time < seconds) then
            time = time + CS.UnityEngine.Time.deltaTime
            coroutine.yield()
        else
            break
        end
    end
end

-- 装饰器节点
---@class DecoratorNode: BTNode
DecoratorNode = BTNode:New('Decorator')

function DecoratorNode:New(name, decoratorFunc)
    local node = BTNode.New(self, name)
    node.decoratorFunc = decoratorFunc
    return node
end

function DecoratorNode:Execute()
    if (#self.children > 0) then
        return self.decoratorFunc(self.children[1])
    end
    return BTState.Failure
end