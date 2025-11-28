using System.IO;
using Controller;
using SaveData;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SaveData
{
    public class SaveDataItem : MonoBehaviour
    {
        public Text saveSlotName;
        public Text saveSlotTime;
        public GameObject saveExistTab;
        public GameObject saveDontExistTab;
        public int saveSlot;
        private string SavePath => SaveDataManager.GetSavePath(saveSlot);
        public void LoadInfo()
        {
            if (File.Exists(SavePath))
            {
                saveExistTab.SetActive(true);
                saveDontExistTab.SetActive(false);
                
                var data = SaveDataManager.Instance.GetSaveFileData(saveSlot);
                
                saveSlotName.text = $"#{Path.GetFileNameWithoutExtension(SavePath)}";
                saveSlotTime.text = data.saveTime;
            }
            else
            {
                saveExistTab.SetActive(false);
                saveDontExistTab.SetActive(true);
            }
        }
        public void SaveData()
        {
            SaveDataManager.Instance.SaveGame(saveSlot);
            LoadInfo();
        }
        public void NewEmptySaveData()
        {
            SaveDataManager.Instance.NewEmptySaveGame(saveSlot);
            LoadInfo();
            SaveDataPanel.Instance.Refresh();
        }
        public void LoadData()
        {
            CameraController.Instance.Reset();
            SaveDataManager.Instance.RestartSceneAndLoad(saveSlot);
        }
        public void DeleteData()
        {
            if (SaveDataManager.Instance.currentSaveFileSlot == saveSlot)
            {
                SaveDataManager.Instance.currentSaveFileSlot = SaveDataManager.DefaultSaveFileSlot;
            }
            File.Delete(SavePath);
            SaveDataPanel.Instance.Refresh();
        }
        public void SaveToNew()
        {
            SaveDataManager.Instance.SaveGame(saveSlot);
            SaveDataPanel.Instance.Refresh();
        }
    }
}