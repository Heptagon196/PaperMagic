using System;
using Backpack;
using UI.General;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Backpack
{
    public enum ItemStat
    {
        Backpack,
        Equipped,
        SpellPanel,
    }
    public interface IItemButtonExtraData {}
    public class ItemButton : MonoBehaviour,
        IBeginDragHandler, IEndDragHandler, IDragHandler,
        IDropHandler,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler
    {
        public bool enableDrag = true;
        public bool enableDrop = true;
        public bool enableTip = true;
        public bool enableRightClick = true;
        public ItemStat stat;
        public BackpackSlot itemType;
        public string itemID;
        public int itemCount;
        public IItemButtonExtraData ExtraData = null;
        
        public Sprite undeterminedSprite;
        public Button button;
        public Image image;
        public Text countText;
        public string itemName;
        public string itemDesc;
        public Action<ItemButton, ItemButton> OnDragTo = null;
        public Action<ItemButton, ItemButton> OnDragFrom = null;
        public Action<ItemButton> OnRightClick = null;
        private Canvas _canvas;
        private Outline _outline;
        private static GameObject _dragItem = null;
        private static RectTransform _draggingRect = null;
        private bool _isDragging = false;
        private bool _isShowingTip = false;
        private void Awake()
        {
            image.sprite = undeterminedSprite;
            _canvas = GetComponentInParent<Canvas>();
            if (_outline == null)
            {
                _outline = GetComponentInChildren<Outline>();
                _outline.enabled = false;
            }
            if (_dragItem == null)
            {
                _dragItem = new GameObject("DragItem");
                _dragItem.transform.SetParent(_canvas.transform);
                _dragItem.AddComponent<RectTransform>();
                _dragItem.AddComponent<Image>().raycastTarget = false;
                _dragItem.AddComponent<Outline>();
                _dragItem.transform.SetAsLastSibling();
                _draggingRect = _dragItem.GetComponent<RectTransform>();
                _dragItem.SetActive(false);
            }
        }
        public void EnableOutline(bool toEnable = true)
        {
            if (_outline == null)
            {
                _outline = GetComponentInChildren<Outline>();
            }
            _outline.enabled = toEnable;
        }
        public void Init(ItemStat inStat, bool inEnableDrag, bool inEnableDrop, bool inEnableTip, bool inEnableRightClick)
        {
            stat = inStat;
            enableDrag = inEnableDrag;
            enableDrop = inEnableDrop;
            enableTip = inEnableTip;
            enableRightClick = inEnableRightClick;
        }
        private void OnDisable()
        {
            if (_isDragging)
            {
                OnEndDrag(null);
            }
        }
        public void LoadInfo(BackpackSlot type, string id, int count, string replaceDesc = null)
        {
            itemType = type;
            itemID = id;
            itemCount = count;
            if (id == null)
            {
                image.sprite = undeterminedSprite;
                itemDesc = replaceDesc ?? "";
                return;
            }

            var data = BackpackManager.Instance.GetItemInfo(type, id);
            if (data == null)
            {
                return;
            }
            var spritePath = data.GetIconPath();
            itemName = data.GetName();
            itemDesc = replaceDesc ?? data.GetDesc();
            if (!string.IsNullOrEmpty(spritePath))
            {
                SpriteLoader.Instance.AsyncLoadSingeSprite(spritePath, image);
            }

            if (countText != null)
            {
                countText.text = count.ToString();
                countText.enabled = count != 1;
            }

            if (_isShowingTip)
            {
                UIFunctions.Instance.UpdateItemTip(itemDesc, 0);
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!enableDrag)
            {
                return;
            }
            var rect = GetComponent<RectTransform>();
            _draggingRect.gameObject.SetActive(true);
            _draggingRect.CopyRectTransformFrom(rect);
            _draggingRect.GetComponent<Image>().sprite = image.sprite;
            
            var color = image.color;
            color.a = 0.5f;
            image.color = color;
            image.raycastTarget = false;
            _isDragging = true;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!enableDrag)
            {
                return;
            }
            var color = image.color;
            color.a = 1f;
            image.color = color;
            image.raycastTarget = true;
            _draggingRect.gameObject.SetActive(false);
            _isDragging = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!enableDrag)
            {
                return;
            }
            _draggingRect.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (enableDrop)
            {
                var infoComp = eventData.pointerDrag.GetComponent<ItemButton>();
                OnDragTo?.Invoke(infoComp, this);
                OnDragFrom?.Invoke(this, infoComp);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enableTip)
            {
                _isShowingTip = true;
                Vector3[] corners = new Vector3[4];
                GetComponent<RectTransform>().GetWorldCorners(corners);
                UIFunctions.Instance.ShowItemTip(itemDesc, corners[2] + new Vector3(5, 0), corners[3].x - corners[0].x);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (enableTip)
            {
                _isShowingTip = false;
                UIFunctions.Instance.tipWidget.GetComponent<KeepActiveWhenHovering>().SetToDisappear();
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (enableRightClick &&
                eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick?.Invoke(this);
            }
        }
    }
}