using System;
using System.Collections.Generic;
using PMLua;
using UnityEngine;

namespace NPC
{
    public class CreatureManager : MonoBehaviour
    {
        public static CreatureManager Instance = null;
        public GameObject creaturePrefab;
        private static readonly Dictionary<string, CreatureInfoBase> CreatureInfoList = new();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
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
                obj.GetComponent<CreatureBehaviour>().InitCreature(info);
                return obj;
            }
            Debug.Log($"No such creature: {creatureType}");
            return null;
        }
    }
}