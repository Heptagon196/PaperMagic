using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UI.Backpack;
using UI.General;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ChatBox
{
    public class ChatBox : MonoBehaviour
    {
        public static ChatBox Instance;
        public Transform optionsPanel;
        public GameObject optionPrefab;
        public Transform textPanel;
        public Text textBox;
        public GameObject interactableTip;
        public GameObject shopUIRoot;
        public Transform shopPanel;
        public GameObject shopItemPrefab;
        public int selectedOption;
        public float textDeltaTime = 0.05f;
        public float unclickableTime = 1f;
        private bool _pressedLeftMouseButton = false;
        public static readonly Dictionary<string, IChatBoxProvider> SuffixToProvider = new()
        {
            { ".lua", new LuaChatBoxProvider() }
        };
        private IChatBoxProvider _currentProvider;
        public static bool ChatBoxOpen =>
            Instance != null &&
            Instance._currentProvider != null;
        private bool _canInteract = false;
        private string _interactableID;
        private Action _interactCallBack;
        private string _runningScriptPath;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            StartCoroutine(UIFunctions.BlinkButtonOpacity(interactableTip, 0.05f, 1.5f, 0.3f, 1));
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _pressedLeftMouseButton = true;
            }
            if (_canInteract)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    interactableTip.SetActive(false);
                    _canInteract = false;
                    _interactCallBack.Invoke();
                    _interactableID = null;
                    _interactCallBack = null;
                }
            }
        }
        public IEnumerator ShowText(string text)
        {
            textBox.text = "";
            float passedTime = 0;
            foreach (var ch in text)
            {
                textBox.text += ch;
                _pressedLeftMouseButton = false;
                yield return new WaitForSeconds(textDeltaTime);
                passedTime += textDeltaTime;
                if (_pressedLeftMouseButton && passedTime >= unclickableTime)
                {
                    textBox.text = text;
                    yield break;
                }
            }
        }
        public IEnumerator ShowOptions(List<string> options)
        {
            selectedOption = -1;
            UIFunctions.ResizeContainer(optionsPanel, optionPrefab, options.Count, null);
            for (var idx = 0; idx < options.Count; idx++)
            {
                var obj = optionsPanel.GetChild(idx);
                var text = obj.GetComponentInChildren<Text>();
                var button = obj.GetComponentInChildren<Button>();
                if (text != null)
                {
                    text.text = options[idx];
                }
                if (button != null)
                {
                    var buttonIdx = idx;
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() =>
                    {
                        selectedOption = buttonIdx;
                    });
                }
            }
            optionsPanel.gameObject.SetActive(true);
            yield return new WaitUntil(() => selectedOption >= 0);
            optionsPanel.gameObject.SetActive(false);
        }
        private IEnumerator _RunScript(IChatBoxProvider provider, string scriptPath)
        {
            _currentProvider = provider;
            _runningScriptPath = scriptPath;
            yield return provider.StartChat(scriptPath);
            _runningScriptPath = null;
            _currentProvider = null;
        }
        public void RunScript(string scriptPath)
        {
            if (IsRunningChat())
            {
                UIFunctions.Instance.ShowFloatTip("正在其它对话中");
                return;
            }
            var extension = Path.GetExtension(scriptPath);
            if (SuffixToProvider.TryGetValue(extension, out var provider))
            {
                StartCoroutine(_RunScript(provider, scriptPath));
            }
        }
        public bool IsRunningChat()
        {
            return _currentProvider != null;
        }
        public bool IsRunningChat(string scriptPath)
        {
            return _currentProvider != null && _runningScriptPath == scriptPath;
        }
        public void CloseChat()
        {
            if (_currentProvider != null)
            {
                _currentProvider.CloseChat();
            }
        }
        public void SetInteractable(string id, bool interactable, Action callback)
        {
            if (interactable)
            {
                interactableTip.SetActive(true);
                _interactableID = id;
                _interactCallBack = callback;
                _canInteract = true;
            }
            else if (_interactableID == id)
            {
                interactableTip.SetActive(false);
                _interactableID = null;
                _interactCallBack = null;
                _canInteract = false;
            }
        }
        public void OpenShop(int itemCount)
        {
            UIFunctions.ResizeContainer(shopPanel, shopItemPrefab, itemCount, item =>
            {
                var itemButton = item.GetComponentInChildren<ItemButton>();
                // itemButton.GetComponent<Image>().raycastTarget = false;
                // itemButton.GetComponentInChildren<Text>().raycastTarget = false;
                itemButton.EnableOutline();
                itemButton.Init(ItemStat.Shop, false, false, true, false);
            });
            shopUIRoot.SetActive(true);
        }
        public Transform GetShopItem(int idx)
        {
            return shopPanel.GetChild(idx);
        }
        public bool ShopIsOpen()
        {
            return shopUIRoot.activeSelf;
        }
        public void CloseShop()
        {
            shopUIRoot.SetActive(false);
        }
    }
}