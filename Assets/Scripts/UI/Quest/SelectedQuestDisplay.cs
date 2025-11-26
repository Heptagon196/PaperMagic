using System;
using Controller;
using Quest;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Quest
{
    public class SelectedQuestDisplay : MonoBehaviour
    {
        private Text _text;
        private Image _background;
        private void Awake()
        {
            _text = GetComponentInChildren<Text>();
            _background = GetComponent<Image>();
        }
        private void Start()
        {
            EventManager.AddListener(this, QuestNotifyEvent.SelectedQuestChanged, param => UpdateText());
            UpdateText();
        }
        private void OnDestroy()
        {
            EventManager.RemoveListeners(this, QuestNotifyEvent.SelectedQuestChanged);
        }
        public void UpdateText()
        {
            if (string.IsNullOrEmpty(QuestManager.SelectedQuest))
            {
                _background.enabled = false;
                _text.text = "";
            }
            else
            {
                _background.enabled = true;
                _text.text = QuestPanel.GetQuestFullDetail(QuestManager.SelectedQuest);
            }
        }
    }
}