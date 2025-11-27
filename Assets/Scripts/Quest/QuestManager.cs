using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backpack;
using Controller;
using PMLua;
using SaveData;
using UI.General;
using UnityEngine;

namespace Quest
{
    public class QuestManager : MonoBehaviour, ISaveDataProcesser
    {
        public static QuestManager Instance;
        public static bool DataLoaded = false;
        private static string _selectedQuest;
        public static string SelectedQuest
        {
            get => _selectedQuest;
            set
            {
                _selectedQuest = value;
                EventManager.Broadcast(QuestNotifyEvent.SelectedQuestChanged);
            }
        }
        // 任务信息
        private static readonly Dictionary<string, QuestInfo> QuestInfos = new();
        // 任务存档数据
        private static readonly Dictionary<string, QuestSaveData> QuestSaveData = new();
        // 缓存任务完成状态
        private static readonly Dictionary<string, QuestStatus> QuestSaveStatus = new();
        // 已接取任务
        private static readonly Dictionary<QuestCategory, List<string>> ActivatedQuests = new();
        private static readonly List<string> CachedActivatedQuests = new();
        // 完成后触发任务
        private static readonly Dictionary<string, List<string>> ToCheckAfterComplete = new();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                EventManager.AddListener(this, BackpackEvent.BackpackChanged,
                    _ => OnRecvEvent(new EventParamBase(QuestNotifyEvent.BackpackChanged)));
                EventManager.AddListener(this, BackpackEvent.EquipChanged,
                    _ => OnRecvEvent(new EventParamBase(QuestNotifyEvent.BackpackChanged)));
                EventManager.AddListener(this, UIPanelEvent.CloseUI,
                    _ => OnRecvEvent(new EventParamBase(QuestNotifyEvent.CloseBackpack)));
                EventManager.AddListener(this, QuestNotifyEvent.EnemyKill, OnRecvEvent);
                EventManager.AddListener(this, QuestNotifyEvent.QuestChatFinish, OnRecvEvent);
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        private void OnDestroy()
        {
            EventManager.RemoveListeners(this, BackpackEvent.BackpackChanged);
            EventManager.RemoveListeners(this, BackpackEvent.EquipChanged);
            EventManager.RemoveListeners(this, UIPanelEvent.CloseUI);
            EventManager.RemoveListeners(this, QuestNotifyEvent.EnemyKill);
            EventManager.RemoveListeners(this, QuestNotifyEvent.QuestChatFinish);
        }
        private static void OnRecvEvent(EventParamBase param)
        {
            if (param.EventType is QuestNotifyEvent recvEventType)
            {
                var updateList = ActivatedQuests
                    .Where(activatedQuest =>
                        activatedQuest.Key != QuestCategory.QuestGoal)
                    .SelectMany(activatedQuest => activatedQuest.Value).ToList();
                foreach (var questID in updateList.Where(questID =>
                             GetQuestInfo(questID)?.notifyEvents.Contains(recvEventType) ?? false))
                {
                    GetQuestStatus(questID, recvEventType, param.Subject, param.ObjectID);
                }
            }
            if (!string.IsNullOrEmpty(SelectedQuest))
            {
                EventManager.Broadcast(QuestNotifyEvent.SelectedQuestChanged);
            }
        }
        public static List<string> GetActivatedQuestsOf(QuestCategory category)
        {
            return ActivatedQuests.GetValueOrDefault(category);
        }
        public static void AddQuest(string id)
        {
            var info = GetQuestInfo(id);
            if (info != null)
            {
                if (!ActivatedQuests.ContainsKey(info.category) ||
                    !ActivatedQuests[info.category].Contains(id))
                {
                    ActivatedQuests.TryAdd(info.category, new());
                    ActivatedQuests[info.category].Add(id);
                    info.OnActivateQuest();
                }
            }
        }
        public static void RemoveQuest(string id)
        {
            var info = GetQuestInfo(id);
            if (info != null)
            {
                ActivatedQuests[info.category].Remove(id);
            }
        }
        public static QuestInfo GetQuestInfo(string id)
        {
            return QuestInfos.GetValueOrDefault(id);
        }
        public static QuestStatus GetCachedQuestStatus(string id)
        {
            return QuestSaveStatus.GetValueOrDefault(id, QuestStatus.None);
        }
        public static QuestStatus GetQuestStatus(string id, QuestNotifyEvent notifyEvent = QuestNotifyEvent.None,
            GameObject questParam = null, string objID = null)
        {
            if (QuestSaveStatus.TryGetValue(id, out var status))
            {
                if (status == QuestStatus.Completed ||
                    status == QuestStatus.Failed)
                {
                    return status;
                }
            }
            var info = GetQuestInfo(id);
            if (info == null)
            {
                return QuestStatus.None;
            }
            bool bChildAllCompleted = true;
            if (info.subQuests != null)
            {
                foreach (var subquest in info.subQuests)
                {
                    var childInfo = GetQuestInfo(subquest);
                    if (childInfo == null)
                    {
                        continue;
                    }
                    var result = GetQuestStatus(subquest, notifyEvent, questParam, objID);
                    if (childInfo.optional)
                    {
                        continue;
                    }
                    if (result == QuestStatus.Completed)
                    {
                        continue;
                    }
                    bChildAllCompleted = false;
                }
            }

            var ret = info.GetQuestStatus(bChildAllCompleted, notifyEvent, questParam, objID);
            if (ret == QuestStatus.Completed || ret == QuestStatus.Failed)
            {
                QuestSaveStatus[id] = ret;
                if (info.category != QuestCategory.QuestGoal)
                {
                    SelectedQuest = id;
                }
            }
            string newAddQuest = null;
            if (ret == QuestStatus.Completed)
            {
                if (ToCheckAfterComplete.TryGetValue(id, out var checkQuest))
                {
                    foreach (var check in checkQuest)
                    {
                        var checkInfo = GetQuestInfo(check);
                        if (checkInfo == null)
                        {
                            continue;
                        }

                        var bAllCompleted = checkInfo.requireQuests.All(require =>
                            GetCachedQuestStatus(require) == QuestStatus.Completed);
                        if (bAllCompleted)
                        {
                            AddQuest(check);
                            newAddQuest = check;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(newAddQuest))
            {
                SelectedQuest = newAddQuest;
            }
            return ret;
        }
        public static void ReadAllQuestsInfo()
        {
            ReadAllLuaQuestsInfo();
            DataLoaded = true;
            TryLoadCachedActivatedQuests();
            ToCheckAfterComplete.Clear();
            foreach (var questInfo in QuestInfos)
            {
                foreach (var require in questInfo.Value.requireQuests)
                {
                    ToCheckAfterComplete.TryAdd(require, new());
                    ToCheckAfterComplete[require].Add(questInfo.Key);
                }
            }
        }
        private static void ReadAllLuaQuestsInfo()
        {
            QuestInfos.Clear();
            LuaManager.RegisterAllLuaScriptsOf("Quests", (id, path, module) =>
            {
                QuestInfos.Add(
                    id,
                    new QuestInfoLua(module)
                );
            });
        }
        public void SaveDataTo(ref GameData gameData)
        {
            gameData.questSave.Clear();
            foreach (var pair in QuestSaveData)
            {
                gameData.questSave.Add(new QuestSaveDataSerializable(pair.Key, pair.Value));
            }
            gameData.questStatus.Clear();
            foreach (var status in QuestSaveStatus)
            {
                gameData.questStatus.Add(new QuestSavePair<QuestStatus>(status));
            }
            gameData.activatedQuests.Clear();
            foreach (var category in ActivatedQuests)
            {
                foreach (var quest in category.Value)
                {
                    gameData.activatedQuests.Add(quest);
                }
            }
            gameData.selectedQuest = SelectedQuest;
        }
        public void LoadDataFrom(ref GameData gameData)
        {
            QuestSaveData.Clear();
            foreach (var save in gameData.questSave)
            {
                var data = save.ToSaveData();
                QuestSaveData.Add(save.id, data);
            }
            QuestSaveStatus.Clear();
            foreach (var status in gameData.questStatus)
            {
                QuestSaveStatus.Add(status.key, status.val);
            }
            ActivatedQuests.Clear();
            CachedActivatedQuests.Clear();
            foreach (var quest in gameData.activatedQuests)
            {
                CachedActivatedQuests.Add(quest);
            }
            SelectedQuest = gameData.selectedQuest;
            TryLoadCachedActivatedQuests();
        }
        private static void TryLoadCachedActivatedQuests()
        {
            if (ActivatedQuests.Count > 0)
            {
                return;
            }
            if (!DataLoaded)
            {
                return;
            }
            foreach (var quest in CachedActivatedQuests)
            {
                var info = GetQuestInfo(quest);
                ActivatedQuests.TryAdd(info.category, new());
                ActivatedQuests[info.category].Add(quest);
            }
        }
        public void SetDefaultData(ref GameData gameData)
        {
            gameData.questSave.Clear();
            gameData.questStatus.Clear();
            gameData.activatedQuests.Clear();
            gameData.selectedQuest = "";
        }
        public static bool GetBool(string module, string key)
        {
            QuestSaveData.TryAdd(module, new QuestSaveData());
            return QuestSaveData[module].SaveBool.GetValueOrDefault(key, false);
        }
        public static void SetBool(string module, string key, bool value)
        {
            QuestSaveData.TryAdd(module, new QuestSaveData());
            QuestSaveData[module].SaveBool[key] = value;
        }
        public static float GetFloat(string module, string key)
        {
            QuestSaveData.TryAdd(module, new QuestSaveData());
            return QuestSaveData[module].SaveFloat.GetValueOrDefault(key, 0);
        }
        public static void SetFloat(string module, string key, float value)
        {
            QuestSaveData.TryAdd(module, new QuestSaveData());
            QuestSaveData[module].SaveFloat[key] = value;
        }
        public static string GetString(string module, string key)
        {
            QuestSaveData.TryAdd(module, new QuestSaveData());
            return QuestSaveData[module].SaveString.GetValueOrDefault(key, "");
        }
        public static void SetString(string module, string key, string value)
        {
            QuestSaveData.TryAdd(module, new QuestSaveData());
            QuestSaveData[module].SaveString[key] = value;
        }
    }
}