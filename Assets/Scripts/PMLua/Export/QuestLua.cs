using Quest;
using XLua;

namespace PMLua.Export
{
    [LuaCallCSharp]
    public class QuestLua
    {
        public int GetStatus(string id)
        {
            return (int)QuestManager.GetQuestStatus(id);
        }
        public void Activate(string id)
        {
            QuestManager.AddQuest(id);
        }
        public bool GetBool(string module, string key)
        {
            return QuestManager.GetBool(module, key);
        }
        public void SetBool(string module, string key, bool value)
        {
            QuestManager.SetBool(module, key, value);
        }
        public float GetFloat(string module, string key)
        {
            return QuestManager.GetFloat(module, key);
        }
        public void SetFloat(string module, string key, float value)
        {
            QuestManager.SetFloat(module, key, value);
        }
        public string GetString(string module, string key)
        {
            return QuestManager.GetString(module, key);
        }
        public void SetString(string module, string key, string value)
        {
            QuestManager.SetString(module, key, value);
        }
    }
}