using System.Collections.Generic;
using Backpack;
using Controller;
using UI.General;
using UnityEngine;

namespace UI.Backpack
{
    public class BackpackPanel : MonoBehaviour
    {
        public List<BackpackSlot> slotTypes;
        public GameObject content;
        public GameObject itemPrefab;
        private void Awake()
        {
            EventManager.AddListener(this, BackpackEvent.BackpackChanged, _ => Refresh());
            EventManager.AddListener(this, UIPanelEvent.OpenUI, _ => Refresh());
        }
        private void OnDestroy()
        {
            EventManager.RemoveListeners(this, BackpackEvent.BackpackChanged);
            EventManager.RemoveListeners(this, UIPanelEvent.OpenUI);
        }
        private void Start()
        {
            Refresh();
        }
        private void ResizeTo(int count)
        {
            UIFunctions.ResizeContainer(content.transform, itemPrefab, count, child =>
            {
                var item = child.GetComponent<ItemButton>();
                item.Init(ItemStat.Backpack, true, true, true, true);
            });
        }
        private ItemButton GetItem(int idx)
        {
            ResizeTo(idx + 1);
            return content.transform.GetChild(idx).GetComponent<ItemButton>();
        }
        private void Refresh()
        {
            if (!UIFunctions.Instance.UIOpen)
            {
                return;
            }

            int count = 0;
            int idx = 0;
            foreach (var slotType in slotTypes)
            {
                var allData = BackpackManager.Instance.GetData(slotType);
                count += allData.Count;
                ResizeTo(count);
                foreach (var data in allData)
                {
                    var item = GetItem(idx);
                    item.gameObject.SetActive(true);
                    item.OnRightClick = null;
                    item.EnableOutline();
                    if (slotType == BackpackSlot.Item)
                    {
                        var info = BackpackManager.Instance.GetItemInfo(slotType, data.Key);
                        if (info is NormalItemInfo itemInfo)
                        {
                            item.OnRightClick = _ =>
                            {
                                var num = itemInfo.UseItem();
                                BackpackManager.Instance.AddNum(slotType, data.Key, -num);
                            };
                        }
                    }
                    item.LoadInfo(slotType, data.Key, data.Value);
                    idx++;
                }
            }
        }
    }
}