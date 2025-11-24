require('Lib/Backpack.lua')
return ChatBox:New()
    :Text('这是一个测试商人')
    :Text('要进入商店吗', false)
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
            AddShopSpell('std.default', 1, 100, 10),
            AddShopSpell('std.triple_cast', 1, 300, -1),
        }
    )
    :Text('再见')
    :Close()