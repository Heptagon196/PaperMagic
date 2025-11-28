using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Backpack;
using Controller;
using PMLua;
using UI.Backpack;
using UI.General;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XLua;

namespace UI.ChatBox
{
    [CSharpCallLua, LuaCallCSharp]
    public interface ILuaChatLine
    {
        public IEnumerator ExecuteLine();
    }
    [CSharpCallLua, LuaCallCSharp]
    public class LuaChatText : ILuaChatLine
    {
        private readonly string _text;
        private readonly bool _click;
        public LuaChatText(string text, bool click = true)
        {
            _text = text;
            _click = click;
        }
        public IEnumerator ExecuteLine()
        {
            yield return ChatBox.Instance.ShowText(_text);
            if (_click)
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
        }
    }
    [CSharpCallLua, LuaCallCSharp]
    public class LuaChatOptions : ILuaChatLine
    {
        private readonly List<LuaChatCreator> _options;
        public LuaChatOptions(List<LuaChatCreator> options)
        {
            _options = options;
        }
        public IEnumerator ExecuteLine()
        {
            List<string> param = _options.Select(option => option.Name).ToList();
            yield return ChatBox.Instance.ShowOptions(param);
            var ret = ChatBox.Instance.selectedOption;
            if (ret < _options.Count && ret >= 0)
            {
                yield return _options[ret].ExecuteLine();
            }
        }
    }
    [CSharpCallLua, LuaCallCSharp]
    public class LuaChatAction : ILuaChatLine
    {
        private Action _action;
        public LuaChatAction(Action action)
        {
            _action = action;
        }
        public IEnumerator ExecuteLine()
        {
            _action?.Invoke();
            yield break;
        }
    }
    [CSharpCallLua, LuaCallCSharp]
    public class LuaChatClose : ILuaChatLine
    {
        public IEnumerator ExecuteLine()
        {
            LuaChatCreator.IsRunning = false;
            yield break;
        }
    }
    [CSharpCallLua, LuaCallCSharp]
    public class LuaChatCreator : ILuaChatLine
    {
        public static bool IsRunning = false;
        public readonly string Name;
        public bool IsRoot = false;
        public LuaChatCreator(string name) => Name = name;
        public LuaChatCreator New(string name) => new LuaChatCreator(name);
        public LuaChatCreator New() => new LuaChatCreator("unnamed");
        private readonly List<ILuaChatLine> _options = new();
        public LuaChatCreator Text(string text, bool click = true)
        {
            _options.Add(new LuaChatText(text, click));
            return this;
        }
        public LuaChatCreator Switch(List<LuaChatCreator> options)
        {
            _options.Add(new LuaChatOptions(options));
            return this;
        }
        public LuaChatCreator Action(Action action)
        {
            _options.Add(new LuaChatAction(action));
            return this;
        }
        public LuaChatCreator Shop(string shopID, List<LuaChatShopItem> shopItems)
        {
            _options.Add(new LuaChatOpenShop(shopID, shopItems));
            return this;
        }
        public LuaChatCreator Close()
        {
            _options.Add(new LuaChatClose());
            return this;
        }
        public IEnumerator ExecuteLine()
        {
            if (IsRoot)
            {
                IsRunning = true;
            }
            foreach (var option in _options)
            {
                yield return option.ExecuteLine();
                if (!IsRunning)
                {
                    yield break;
                }
            }
            if (IsRoot)
            {
                IsRunning = false;
            }
        }
    }
    [CSharpCallLua, LuaCallCSharp]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LuaChatShopItem
    {
        public int SellSlot;
        public string SellID;
        public int SellCount;
        public int BuySlot;
        public string BuyID;
        public int BuyCount;
        public int BuyLimit;
    }
    [CSharpCallLua, LuaCallCSharp]
    public class LuaChatOpenShop : ILuaChatLine
    {
        private readonly string _shopID;
        private readonly List<LuaChatShopItem> _options;
        public LuaChatOpenShop(string shopID, List<LuaChatShopItem> options)
        {
            _shopID = shopID;
            _options = options;
        }
        public IEnumerator ExecuteLine()
        {
            ChatBox.Instance.OpenShop(_options.Count);
            ShopManager.TrySetShopBuyLimit(_shopID, _options.Select(data => data.BuyLimit).ToList());
            for (var idx = 0; idx < _options.Count; idx++)
            {
                var data = _options[idx];
                var item = ChatBox.Instance.GetShopItem(idx);
                
                var itemButton = item.GetComponentInChildren<ItemButton>();
                itemButton.LoadInfo((BackpackSlot)data.SellSlot, data.SellID, data.SellCount);
                
                var desc = item.GetChild(0).GetComponent<Text>();
                var sellInfo = BackpackManager.Instance.GetItemInfo((BackpackSlot)data.SellSlot, data.SellID);
                var buyInfo = BackpackManager.Instance.GetItemInfo((BackpackSlot)data.BuySlot, data.BuyID);
                var buyItemDesc = $"{data.BuyCount} {buyInfo.GetName()}";
                desc.text = buyItemDesc;
                var startLimit = ShopManager.GetShopBuyLimit(_shopID, idx);
                if (startLimit != -1)
                {
                    desc.text += $"\n限购 {startLimit} 次";
                }
                else
                {
                    desc.text += "\n不限购";
                }

                var shopItemIdx = idx;
                UnityAction listener = () =>
                {
                    var playerData = BackpackManager.Instance.GetNum((BackpackSlot)data.BuySlot, data.BuyID);
                    if (playerData < data.BuyCount)
                    {
                        UIFunctions.Instance.ShowFloatTip($"{buyInfo.GetName()}数量不足！");
                        return;
                    }
                    var limit = ShopManager.GetShopBuyLimit(_shopID, shopItemIdx);
                    if (limit == 0)
                    {
                        UIFunctions.Instance.ShowFloatTip("已达到购买次数上限");
                        return;
                    }
                    UIFunctions.Instance.ShowConfirmBox($"确认要购买{data.SellCount}个{sellInfo.GetName()}吗", () =>
                    {
                        BackpackManager.Instance.AddNum((BackpackSlot)data.BuySlot, data.BuyID, -data.BuyCount);
                        BackpackManager.Instance.AddNum((BackpackSlot)data.SellSlot, data.SellID, data.SellCount);
                        UIFunctions.Instance.ShowFloatTip("购买成功");
                        if (limit > 0)
                        {
                            limit--;
                            ShopManager.OnBuy(_shopID, shopItemIdx);
                        }
                        if (limit != -1)
                        {
                            desc.text = buyItemDesc + $"\n限购 {limit} 次";
                        }
                    }, null);
                };

                var button = item.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                itemButton.button.onClick.RemoveAllListeners();
                button.onClick.AddListener(listener);
                itemButton.button.onClick.AddListener(listener);
            }
            yield return new WaitUntil(() => !ChatBox.Instance.ShopIsOpen());
        }
    }
    public class LuaChatBoxProvider : IChatBoxProvider
    {
        public bool IsRunning => LuaChatCreator.IsRunning;
        public IEnumerator StartChat(string filePath)
        {
            var script = new LuaScriptExecutor
            {
                luaScriptPath = filePath
            };
            script.InitScriptEnv();
            LuaManager.Env.Global.Set("ChatBox", new LuaChatCreator("template"));
            var objects = script.RawRunScript();
            PlayerController.Instance.playerRigidbody.isKinematic = true;
            if (objects.Length > 0 && objects[0] is LuaChatCreator chatCreator)
            {
                ChatBox.Instance.textPanel.gameObject.SetActive(true);
                chatCreator.IsRoot = true;
                yield return chatCreator.ExecuteLine();
                ChatBox.Instance.textPanel.gameObject.SetActive(false);
            }
            PlayerController.Instance.playerRigidbody.isKinematic = false;
        }
        public void CloseChat()
        {
            LuaChatCreator.IsRunning = false;
        }
    }
}