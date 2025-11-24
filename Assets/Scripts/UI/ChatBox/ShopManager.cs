using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

namespace UI.ChatBox
{
    [Serializable]
    public class ShopBuyLimitSaveLine
    {
        public string shopID;
        public List<int> limitBuy = new();
    }
    public class ShopManager : MonoBehaviour, ISaveDataProcesser
    {
        private static readonly Dictionary<string, List<int>> ShopLimitBuy = new();
        public static void TrySetShopBuyLimit(string shopID, List<int> limitBuy)
        {
            ShopLimitBuy.TryAdd(shopID, limitBuy);
            if (ShopLimitBuy[shopID].Count != limitBuy.Count)
            {
                ShopLimitBuy[shopID] = limitBuy;
            }
        }
        public static int GetShopBuyLimit(string shopID, int index)
        {
            if (!ShopLimitBuy.TryGetValue(shopID, out var limitBuy))
            {
                return -1;
            }
            if (index >= limitBuy.Count)
            {
                return -1;
            }
            return limitBuy[index];
        }
        public static void OnBuy(string shopID, int index, int count = 1)
        {
            if (!ShopLimitBuy.TryGetValue(shopID, out var limitBuy))
            {
                return;
            }
            if (index >= limitBuy.Count)
            {
                return;
            }
            limitBuy[index] -= count;
        }
        public void SaveDataTo(ref GameData gameData)
        {
            gameData.shopLimit.Clear();
            foreach (var shopLimit in ShopLimitBuy)
            {
                gameData.shopLimit.Add(new ShopBuyLimitSaveLine
                {
                    shopID = shopLimit.Key,
                    limitBuy = shopLimit.Value
                });
            }
        }
        public void LoadDataFrom(ref GameData gameData)
        {
            ShopLimitBuy.Clear();
            foreach (var shopLimit in gameData.shopLimit)
            {
                ShopLimitBuy.Add(shopLimit.shopID, shopLimit.limitBuy);
            }
        }
        public void SetDefaultData(ref GameData gameData)
        {
            ShopLimitBuy.Clear();
        }
    }
}