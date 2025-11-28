using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backpack;
using Controller;
using NPC;
using Quest;
using UI.ChatBox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SaveData
{
    [Serializable]
    public class GlobalSettingsData
    {
        public int lastSaveSlot;
    }
    [Serializable]
    public class GameData
    {
        public string saveTime;
        // 玩家
        public PlayerMovementMode movementMode;
        public Vector3 playerPosition;
        public Vector3 cameraPosition;
        public Quaternion cameraRotation;
        public float maxHealth = 100;
        public float currentHealth = 100;
        
        // 背包
        public List<GameDataItem> backpack = new();
        public List<GameDataEquipment> equipped = new();
        
        // 任务
        public List<QuestSaveDataSerializable> questSave = new();
        public List<QuestSavePair<QuestStatus>> questStatus = new();
        public List<string> activatedQuests = new();
        public string selectedQuest;
        
        // 商店限购
        public List<ShopBuyLimitSaveLine> shopLimit = new();
        
        // 敌人
        public List<PersistentCreatureInfo> persistentCreatures = new();
    }
    [Serializable]
    public class GameDataItem
    {
        public BackpackSlot type;
        public string id;
        public int count;
    }
    [Serializable]
    public class GameDataEquipment
    {
        public Equipment.EquipmentSlot slot;
        public EquippedItemInfo info;
    }
    public interface ISaveDataProcesser
    {
        public void SaveDataTo(ref GameData gameData);
        public void LoadDataFrom(ref GameData gameData);
        public void SetDefaultData(ref GameData gameData);
    }
    public class SaveDataManager : MonoBehaviour
    {
        public static SaveDataManager Instance;
        public const string SaveDir = "GameData/SaveData";
        public GameData data;
        private static readonly List<ISaveDataProcesser> Processers = new();
        public const int DefaultSaveFileSlot = 0;
        public const string GlobalSettingsFilePath = SaveDir + "/global.json";
        public int currentSaveFileSlot = DefaultSaveFileSlot;
        public GlobalSettingsData globalSettings;
        public static string GetSavePath(int slot)
        {
            return SaveDir + $"/{slot}.json";
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                data = null;
                FindAllProcessers();
                LoadGlobalSettings();
                LoadGame();
            }
        }
        public void SaveGlobalSettings()
        {
            try
            {
                globalSettings.lastSaveSlot = currentSaveFileSlot;
                
                string path = GlobalSettingsFilePath;
                Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
                string content = JsonUtility.ToJson(globalSettings, true);
                using FileStream fs = File.Create(path);
                using StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void LoadGlobalSettings()
        {
            if (File.Exists(GlobalSettingsFilePath))
            {
                try
                {
                    using FileStream fs = File.OpenRead(GlobalSettingsFilePath);
                    using StreamReader sr = new StreamReader(fs);
                    string content = sr.ReadToEnd();
                    globalSettings = JsonUtility.FromJson<GlobalSettingsData>(content);
                    if (globalSettings == null)
                    {
                        Debug.Log("Global save file corrupted");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            globalSettings ??= new GlobalSettingsData()
            {
                lastSaveSlot = currentSaveFileSlot
            };
            currentSaveFileSlot = globalSettings.lastSaveSlot;
        }
        public void SaveGame(int saveSlot = -1)
        {
            if (saveSlot >= 0)
            {
                currentSaveFileSlot = saveSlot;
            }
            if (data == null)
            {
                Debug.Log("Save Game Failed");
                return;
            }
            data.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            foreach (var processer in Processers)
            {
                processer.SaveDataTo(ref data);
            }
            try
            {
                string path = GetSavePath(currentSaveFileSlot);
                Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
                string content = JsonUtility.ToJson(data, true);
                using FileStream fs = File.Create(path);
                using StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            if (currentSaveFileSlot != DefaultSaveFileSlot)
            {
                File.Copy(GetSavePath(currentSaveFileSlot),
                    GetSavePath(DefaultSaveFileSlot), true);
            }
        }
        public void NewEmptySaveGame(int saveSlot)
        {
            var newSave = new GameData();
            data.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            foreach (var processer in Processers)
            {
                processer.SetDefaultData(ref data);
            }
            try
            {
                string path = GetSavePath(saveSlot);
                Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
                string content = JsonUtility.ToJson(data, true);
                using FileStream fs = File.Create(path);
                using StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public GameData GetSaveFileData(int loadSlot)
        {
            GameData ret = null;
            var loadPath = GetSavePath(loadSlot);
            if (!File.Exists(loadPath))
            {
                return null;
            }
            try
            {
                using FileStream fs = File.OpenRead(loadPath);
                using StreamReader sr = new StreamReader(fs);
                string content = sr.ReadToEnd();
                ret = JsonUtility.FromJson<GameData>(content);
                if (ret == null)
                {
                    Debug.Log("Save file corrupted");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return ret;
        }
        public void RestartSceneAndLoad(int loadSlot = -1)
        {
            var async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            if (async != null)
            {
                async.completed += _ =>
                {
                    LoadGame(loadSlot);
                };
            }
        }
        public void LoadGame(int loadSlot = -1)
        {
            if (loadSlot >= 0)
            {
                currentSaveFileSlot = loadSlot;
            }
            data = GetSaveFileData(currentSaveFileSlot);
            if (data == null)
            {
                data = new()
                {
                    saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                foreach (var processer in Processers)
                {
                    processer.SetDefaultData(ref data);
                }
            }
            if (data.currentHealth <= 0)
            {
                data.currentHealth = data.maxHealth;
            }
            foreach (var processer in Processers)
            {
                processer.LoadDataFrom(ref data);
            }
        }
        public void OnApplicationQuit()
        {
            if (PlayerController.Instance.healthPoint > 0)
            {
                SaveGame();
            }
            SaveGlobalSettings();
        }
        public void FindAllProcessers()
        {
            Processers.Clear();
            IEnumerable<ISaveDataProcesser> processers = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveDataProcesser>();
            Processers.AddRange(processers);
        }
    }
}