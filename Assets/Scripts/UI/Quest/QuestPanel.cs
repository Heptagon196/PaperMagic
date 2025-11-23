using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Quest;
using UI.General;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Quest
{
    [Serializable]
    public struct QuestTypeInfo
    {
        public QuestCategory category;
        public Button button;
    }
    public class QuestPanel : MonoBehaviour
    {
        public QuestTypeInfo[] questInfos;
        public Transform questButtonContainer;
        public GameObject questButtonPrefab;
        public RectTransform questDetailContainer;
        public QuestItem questDetailRoot;
        public GameObject questDetailPrefab;
        public QuestCategory currentCategory = QuestCategory.MainQuest;
        public string showingQuestID;
        private void Start()
        {
            foreach (var questType in questInfos)
            {
                questType.button.onClick.AddListener(() =>
                {
                    currentCategory = questType.category;
                    ShowQuestList();
                    ShowQuestDetail(null);
                });
            }
            ShowQuestList();
        }
        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(showingQuestID))
            {
                ShowQuestList();
                ShowQuestDetail(showingQuestID);
            }
        }
        public void ShowQuestList()
        {
            var questList = QuestManager.GetActivatedQuestsOf(currentCategory) ?? new();
            List<QuestInfo> showList = new();
            List<QuestInfo> completedList = new();
            foreach (var questInfo in questList.Select(QuestManager.GetQuestInfo).OrderBy(
                         questInfo => questInfo.sortOrder))
            {
                var status = QuestManager.GetQuestStatus(questInfo.id);
                if (status == QuestStatus.Failed || status == QuestStatus.Completed)
                {
                    completedList.Add(questInfo);
                }
                if (status == QuestStatus.NotCompleted)
                {
                    showList.Add(questInfo);
                }
            }
            showList.AddRange(completedList);
            UIFunctions.ResizeContainer(questButtonContainer, questButtonPrefab, showList.Count, null);
            for (int idx = 0; idx < showList.Count; idx++)
            {
                var questInfo = showList[idx];
                var child = questButtonContainer.GetChild(idx);
                child.GetComponentInChildren<Text>().text = questInfo.questName;
                var button = child.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ShowQuestDetail(questInfo.id));
            }
        }
        public void ShowQuestDetail(QuestItem root, string questID, int depth)
        {
            var questInfo = QuestManager.GetQuestInfo(questID);
            if (questInfo == null)
            {
                return;
            }

            var title = "";
            if (depth == 0)
            {
                title = questInfo.questName;
            }
            root.SetText(title, questInfo.GetOverrideDesc(), depth, QuestManager.GetQuestStatus(questID), questInfo.optional);

            List<string> showList = new();
            foreach (var subQuest in questInfo.subQuests)
            {
                var status = QuestManager.GetQuestStatus(subQuest);
                if (status != QuestStatus.Hide && status != QuestStatus.None)
                {
                    showList.Add(subQuest);
                }
            }
            UIFunctions.ResizeContainer(root.container, questDetailPrefab, showList.Count, null);
            for (var idx = 0; idx < showList.Count; idx++)
            {
                var child = root.container.GetChild(idx);
                var questItem = child.GetComponent<QuestItem>();
                ShowQuestDetail(questItem, showList[idx], depth + 1);
            }
        }
        public void ShowQuestDetail(string questID)
        {
            showingQuestID = questID;
            if (string.IsNullOrEmpty(questID))
            {
                questDetailRoot.gameObject.SetActive(false);
            }
            else
            {
                questDetailRoot.gameObject.SetActive(true);
                ShowQuestDetail(questDetailRoot, questID, 0);
                questDetailRoot.CalcWidgetHeight();
            }
        }
    }
}