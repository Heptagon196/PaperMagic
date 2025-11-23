return require('Quests/TemplateQuest/Kill10.lua'):New{
    ID = 'std.default.kill1_special',
    Desc = '击杀1个精英',
    Type = QuestCategory.QuestGoal,
    Optional = true,

    Level = 1,
    TargetCount = 1,
}