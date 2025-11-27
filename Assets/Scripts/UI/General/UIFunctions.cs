using System;
using System.Collections;
using Controller;
using SaveData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    public enum UIPanelEvent
    {
        OpenUI,
        CloseUI,
    }
    public class UIFunctions : MonoBehaviour
    {
        public static UIFunctions Instance;
        public bool initVisibility;
        public GameObject loseGamePanel;
        public Button[] tabToggles;
        public GameObject[] tabs;
        public GameObject tipWidget;
        public UIWidgetPool floatTipPool;
        public ConfirmBox confirmBox;
        public Vector2 floatStartPos;
        public Vector2 floatEndPos;
        public float floatFadeTime;
        public float floatStayTime;
        public bool UIOpen =>
            _rootTransform.gameObject.activeInHierarchy ||
            ChatBox.ChatBox.ChatBoxOpen ||
            loseGamePanel.activeInHierarchy;
        private Canvas _canvas;
        private Transform _rootTransform;
        private void Awake()
        {
            Instance = this;
            tipWidget.SetActive(false);
            _rootTransform = transform.GetChild(0).transform;
            _rootTransform.gameObject.SetActive(initVisibility);
            _canvas = GetComponentInParent<Canvas>();
            for (int idx = 0; idx < tabToggles.Length; idx++)
            {
                var toggleIdx = idx;
                tabToggles[toggleIdx].onClick.AddListener(() =>
                {
                    for (int tabIdx = 0; tabIdx < tabs.Length; tabIdx++)
                    {
                        tabToggles[tabIdx].GetComponent<Outline>().enabled = tabIdx == toggleIdx;
                        tabs[tabIdx].SetActive(tabIdx == toggleIdx);
                    }
                });
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                SwitchUIVisibility();
            }
        }
        public void SwitchUIVisibility()
        {
            _rootTransform.gameObject.SetActive(!_rootTransform.gameObject.activeSelf);
            EventManager.Broadcast(_rootTransform.gameObject.activeSelf ? UIPanelEvent.OpenUI : UIPanelEvent.CloseUI);
        }
        public void UpdateItemTip(string text, float itemWidth)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            tipWidget.GetComponentInChildren<Text>().text = text;
            tipWidget.GetComponent<KeepActiveWhenHovering>().OnAppear();
            var rectTransform = tipWidget.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            var canvasRect = _canvas.GetComponent<RectTransform>().rect;
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            if (corners[3].y < 5)
            {
                rectTransform.transform.position += new Vector3(0, 5 - corners[3].y);
            }
            if (corners[3].x > canvasRect.width)
            {
                rectTransform.transform.position -= new Vector3(rectTransform.rect.width + itemWidth + 10, 0);
            }
        }
        public void ShowItemTip(string text, Vector3 pos, float itemWidth)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            tipWidget.SetActive(true);
            tipWidget.transform.position = pos;
            UpdateItemTip(text, itemWidth);
        }
        public void ShowFloatTip(string text)
        {
            floatTipPool.GetObject().GetComponent<FloatTipWidget>()
                ?.ShowTip(text, floatStartPos, floatEndPos, floatFadeTime, floatStayTime);
        }
        public void ShowConfirmBox(string text, Action onConfirm, Action onCancel)
        {
            confirmBox.ShowConfirmBox(text, onConfirm, onCancel);
        }
        public static void ResizeContainer(Transform container, GameObject prefab, int count,
            Action<GameObject> onInit, int offset = 0)
        {
            while (count + offset > container.childCount)
            {
                var child = Instantiate(prefab, container);
                onInit?.Invoke(child);
            }
            for (int childIdx = offset; childIdx < count + offset; childIdx++)
            {
                container.GetChild(childIdx).gameObject.SetActive(true);
            }
            for (int childIdx = count + offset; childIdx < container.childCount; childIdx++)
            {
                container.GetChild(childIdx).gameObject.SetActive(false);
            }
        }
        public static IEnumerator BlinkButtonOpacity(GameObject button, float deltaTime, float speed, float minVal, float maxVal)
        {
            var image = button.GetComponent<Image>();
            var text = button.GetComponentInChildren<Text>();
            var imageColor = image.color;
            var textColor = text.color;
            imageColor.a = 1;
            textColor.a = 1;
            float direction = -1;
            float destOpacity = minVal;
            float rate = 0;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (button != null)
            {
                rate += deltaTime * speed * direction;
                imageColor.a = Mathf.Lerp(minVal, maxVal, rate);
                if (Mathf.Abs(imageColor.a - destOpacity) <= deltaTime)
                {
                    destOpacity = minVal + maxVal - destOpacity;
                    direction *= -1;
                }
                textColor.a = imageColor.a;
                image.color = imageColor;
                text.color = textColor;
                yield return new WaitForSeconds(deltaTime);
            }
        }
        public void ShowLoseGame()
        {
            loseGamePanel.SetActive(true);
        }
        public void LoadData()
        {
            loseGamePanel.SetActive(false);
            SaveDataManager.Instance.RestartSceneAndLoad();
        }
    }
}
