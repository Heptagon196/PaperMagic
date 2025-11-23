using System;
using System.IO;
using SaveData;
using UI.General;
using UnityEngine;

namespace UI.SaveData
{
    public class SaveDataPanel : MonoBehaviour
    {
        public static SaveDataPanel Instance;
        public GameObject saveDataPrefab;
        private string GetSavePath(int slot) => SaveDataManager.GetSavePath(slot);
        private void Start()
        {
            Instance = this;
            Refresh();
        }
        private void OnEnable()
        {
            if (Instance != null)
            {
                SaveDataManager.Instance.FindAllProcessers();
                Refresh();
            }
        }
        public void Refresh()
        {
            var count = 1;
            var files = Directory.GetFiles(SaveDataManager.SaveDir, "*.json", SearchOption.TopDirectoryOnly);
            foreach (var filePath in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (int.TryParse(fileName, out int slot))
                {
                    count = Math.Max(count, slot + 1);
                }
            }
            UIFunctions.ResizeContainer(transform, saveDataPrefab, count, null);
            for (var childIdx = 0; childIdx < count; childIdx++)
            {
                var child = transform.GetChild(childIdx).gameObject;
                child.SetActive(true);
                var saveItem = child.GetComponent<SaveDataItem>();
                saveItem.saveSlot = childIdx + 1;
                saveItem.LoadInfo();
            }
        }
    }
}