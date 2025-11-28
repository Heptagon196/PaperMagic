---@enum BackpackSlot
BackpackSlot = {
    Equipment = 0,
    Spell = 1,
    Item = 2,
}

function AddShopBuyInfo(slot, id, count, buySlot, buyId, buyCount, buyLimit)
    return {
        SellSlot = slot,
        SellID = id,
        SellCount = count,
        BuySlot = buySlot,
        BuyID = buyId,
        BuyCount = buyCount,
        BuyLimit = buyLimit or -1,
    }
end

function AddShopSpell(id, count, cost, limit)
    return AddShopBuyInfo(BackpackSlot.Spell, id, count, BackpackSlot.Item, 'std.coin', cost, limit)
end

function AddShopEquip(id, cost, limit)
    return AddShopBuyInfo(BackpackSlot.Equipment, id, 1, BackpackSlot.Item, 'std.coin', cost, limit)
end

function AddShopItem(id, count, cost, limit)
    return AddShopBuyInfo(BackpackSlot.Item, id, count, BackpackSlot.Item, 'std.coin', cost, limit)
end