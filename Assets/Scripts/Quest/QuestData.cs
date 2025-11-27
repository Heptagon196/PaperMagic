using System;
using System.Collections.Generic;
using Controller;
using UnityEngine;
using XLua;

namespace Quest
{
    [Serializable]
    public enum QuestNotifyEvent
    {
        None,
        EnemyKill,
        QuestChatFinish,
        BackpackChanged,
        SelectedQuestChanged,
        CloseBackpack,
    }
    [CSharpCallLua]
    public delegate int GetQuestStatusDelegate(LuaTable self, bool childCompleted, int notifyEvent, GameObject obj, string objID);
    [CSharpCallLua]
    public delegate void OnActivateQuestDelegate(LuaTable self);
    [CSharpCallLua]
    public delegate string GetQuestDesc(LuaTable self);
    [Serializable]
    public enum QuestStatus
    {
        None,
        Hide,
        NotCompleted,
        Completed,
        Failed,
    }
    [Serializable]
    public enum QuestCategory
    {
        MainQuest,
        SideQuest,
        QuestGoal,
    }
    [Serializable]
    public abstract class QuestInfo
    {
        public string id;
        public string questName;
        public string questDesc;
        public int sortOrder;
        public QuestCategory category;
        public bool optional;
        public List<string> subQuests;
        public List<QuestNotifyEvent> notifyEvents;
        public List<string> requireQuests;
        public abstract QuestStatus GetQuestStatus(bool childCompleted, QuestNotifyEvent notifyEvent, GameObject obj, string objID);
        public abstract void OnActivateQuest();
        public abstract string GetOverrideDesc();
    }
    public class QuestSaveData
    {
        public readonly Dictionary<string, bool> SaveBool = new();
        public readonly Dictionary<string, float> SaveFloat = new();
        public readonly Dictionary<string, string> SaveString = new();
    }
    [Serializable]
    public struct QuestSavePair<T>
    {
        public string key;
        public T val;
        public QuestSavePair(KeyValuePair<string, T> data)
        {
            key = data.Key;
            val = data.Value;
        }
    }
    [Serializable]
    public class QuestSaveDataSerializable
    {
        public string id;
        public List<QuestSavePair<bool>> b;
        public List<QuestSavePair<float>> f;
        public List<QuestSavePair<string>> s;
        public QuestSaveDataSerializable(string inID, QuestSaveData saveData)
        {
            id = inID;
            b = new();
            foreach (var pair in saveData.SaveBool)
            {
                b.Add(new QuestSavePair<bool>(pair));
            }
            f = new();
            foreach (var pair in saveData.SaveFloat)
            {
                f.Add(new QuestSavePair<float>(pair));
            }
            s = new();
            foreach (var pair in saveData.SaveString)
            {
                s.Add(new QuestSavePair<string>(pair));
            }
        }
        public QuestSaveData ToSaveData()
        {
            var saveData = new QuestSaveData();
            foreach (var pair in b)
            {
                saveData.SaveBool.Add(pair.key, pair.val);
            }
            foreach (var pair in f)
            {
                saveData.SaveFloat.Add(pair.key, pair.val);
            }
            foreach (var pair in s)
            {
                saveData.SaveString.Add(pair.key, pair.val);
            }
            return saveData;
        }
    }
    public class QuestInfoLua : QuestInfo
    {
        private readonly LuaTable _module;
        private readonly GetQuestStatusDelegate _getQuestStatus;
        private readonly OnActivateQuestDelegate _onActivateQuest;
        private readonly GetQuestDesc _onGetQuestDesc;
        private bool _isActivating = false;
        public QuestInfoLua(LuaTable module)
        {
            if (module == null)
            {
                return;
            }
            _module = module;
            id = module.Get<string>("ID");
            questName = module.Get<string>("Name") ?? "";
            questDesc = module.Get<string>("Desc") ?? "";
            category = (QuestCategory)(module.Get<int?>("Type") ?? 0);
            sortOrder = 0;
            if (module.ContainsKey("Sort"))
            {
                sortOrder = module.Get<int>("Sort");
            }
            optional = false;
            if (module.ContainsKey("Optional"))
            {
                optional = module.Get<bool>("Optional");
            }
            subQuests = module.Get<List<string>>("SubQuests") ?? new();
            requireQuests = module.Get<List<string>>("Require") ?? new();
            _getQuestStatus = module.Get<GetQuestStatusDelegate>("GetStatus");
            _onActivateQuest = module.Get<OnActivateQuestDelegate>("OnActivate");
            _onGetQuestDesc = module.Get<GetQuestDesc>("GetDesc");

            var events = module.Get<List<int>>("Notify") ?? new();
            notifyEvents = new();
            foreach (var notifyEvent in events)
            {
                notifyEvents.Add((QuestNotifyEvent)notifyEvent);
            }
        }
        public override QuestStatus GetQuestStatus(bool childCompleted, QuestNotifyEvent notifyEvent, GameObject obj, string objID)
        {
            if (_isActivating)
            {
                return QuestStatus.None;
            }
            if (_getQuestStatus == null)
            {
                return childCompleted ? QuestStatus.Completed : QuestStatus.NotCompleted;
            }
            return (QuestStatus)_getQuestStatus.Invoke(_module, childCompleted, (int)notifyEvent, obj, objID);
        }
        public override void OnActivateQuest()
        {
            _isActivating = true;
            _onActivateQuest?.Invoke(_module);
            _isActivating = false;
        }

        public override string GetOverrideDesc()
        {
            return _onGetQuestDesc?.Invoke(_module) ?? questDesc;
        }
    }
}