using System;
using Backpack;
using Controller;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Player
{
    public class PlayerItemCountIndicator : MonoBehaviour
    {
        public BackpackSlot slotType;
        public string itemID;
        public string formatString;
        private Text _text;
        private void Start()
        {
            _text = GetComponent<Text>();
            _text.enabled = true;
            EventManager.AddListener(this, BackpackEvent.BackpackChanged, _ => UpdateItemCount());
            UpdateItemCount();
        }
        private void OnDestroy()
        {
            EventManager.RemoveListeners(this, BackpackEvent.BackpackChanged);
        }
        private void UpdateItemCount()
        {
            _text.text = string.Format(formatString, BackpackManager.Instance.GetNum(slotType, itemID));
        }
    }
}