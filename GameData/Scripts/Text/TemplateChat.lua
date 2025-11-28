require('Lib/Backpack.lua')
return ChatBox:New()
    :Text('这是一个商店')
    :Text('要购买物品吗', false)
    :Switch{
        ChatBox:New('是'),
        ChatBox:New('否')
            :Close(),
    }
    :Action(function()
        print('进入商店')
    end)
    :Shop('std.template_merchant',
        {
            AddShopItem('std.heal_item', 1, 100, -1),
            AddShopSpell('std.default', 1, 100, -1),
            AddShopSpell('std.add_damage', 1, 100, 10),
            AddShopSpell('std.circle_trail', 1, 100, 10),
            AddShopSpell('std.jump', 1, 100, 10),
            AddShopSpell('std.double_cast', 1, 200, 10),
            AddShopSpell('std.triple_cast', 1, 300, 10),
            AddShopSpell('std.trigger_on_collision', 1, 300, 10),
            AddShopSpell('std.trigger_on_expired', 1, 300, 10),
            AddShopEquip('std.advanced_wand', 500, 10),
        }
    )
    :Text('再见')
    :Close()