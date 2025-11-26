using System;
using System.Collections.Generic;
using PMLua;
using SaveData;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public class PersistentCreatureInfo
    {
        public string id;
        public Vector3 position;
    }
    public class CreatureManager : MonoBehaviour, ISaveDataProcesser
    {
        public static CreatureManager Instance = null;
        public GameObject creaturePrefab;
        private static readonly Dictionary<string, CreatureInfoBase> CreatureInfoList = new();
        public readonly List<CreatureBehaviour> PersistentCreatures = new();
        private List<PersistentCreatureInfo> _spawnList;
        public static void SetCreaturePersistent(GameObject creature)
        {
            Instance.PersistentCreatures.Add(creature?.GetComponent<CreatureBehaviour>());
            Instance.PersistentCreatures.RemoveAll(x => x == null);
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        private void Update()
        {
            if (_spawnList != null)
            {
                foreach (var creature in _spawnList)
                {
                    var obj = SpawnCreature(creature.id, creature.position);
                    if (obj != null)
                    {
                        PersistentCreatures.Add(obj.GetComponent<CreatureBehaviour>());
                    }
                }
                _spawnList = null;
            }
        }
        public static CreatureInfoBase GetCreatureInfo(string id)
        {
            if (CreatureInfoList.TryGetValue(id, out var info))
            {
                return info;
            }
            Debug.Log($"No such creature: {id}");
            return null;
        }
        public static CreatureInfoBase SpawnCreatureInfoByID(string id)
        {
            return GetCreatureInfo(id)?.Clone();
        }
        public static void RegisterAllCreatureInfo()
        {
            LuaManager.RegisterAllLuaScriptsOf("Creatures", (id, path, module) =>
            {
                CreatureInfoList.Add(id, new CreatureInfoLua(module, path));
            });
        }
        public static GameObject SpawnCreature(string creatureType, Vector3 position)
        {
            if (CreatureInfoList.TryGetValue(creatureType, out var info))
            {
                var obj = Instantiate(Instance.creaturePrefab, position, Quaternion.identity);
                obj.GetComponent<CreatureBehaviour>().InitCreature(info.Clone());
                return obj;
            }
            Debug.Log($"No such creature: {creatureType}");
            return null;
        }
        public void SaveDataTo(ref GameData gameData)
        {
            gameData.persistentCreatures.Clear();
            PersistentCreatures.RemoveAll(x => x == null);
            foreach (var creature in PersistentCreatures)
            {
                gameData.persistentCreatures.Add(new PersistentCreatureInfo()
                {
                    id = creature.CreatureInfo.id,
                    position = creature.transform.position,
                });
            }
        }
        public void LoadDataFrom(ref GameData gameData)
        {
            PersistentCreatures.ForEach(creature =>
            {
                if (creature != null)
                {
                    Destroy(creature.gameObject);
                }
            });
            _spawnList = gameData.persistentCreatures;
            PersistentCreatures.Clear();
        }
        public void SetDefaultData(ref GameData gameData)
        {
        }
    }
}