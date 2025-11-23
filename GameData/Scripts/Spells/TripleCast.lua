return require('Spells/DoubleCast.lua'):New{
    ID = 'std.triple_cast',
    Category = SpellType.Modifier,
	Name = '三重释放',
	Desc = '释放三个法术，增加散射',
    Icon = 'Spell/std/triple_cast.png',
    Cost = 15,
	ChildNode = 3,
}