using System.Collections.Generic;
using System.IO;
using System.Linq;
using Equipment;
using PMLua;
using UnityEngine;

namespace Backpack
{
    public static class NormalItemManager
    {
        public const string EmptyItem = "std.empty";
        private static readonly Dictionary<string, NormalItemBase> AllItemSpawner = new();
        public static NormalItemBase SpawnItem(string name)
        {
            var item = TrySpawnItem(name);
            if (item == null)
            {
                Debug.LogError("Item not found: " + name);
            }
            return item;
        }
        public static NormalItemBase TrySpawnItem(string name)
        {
            AllItemSpawner.TryGetValue(name, out NormalItemBase spawnInfo);
            if (spawnInfo == null)
            {
                return null;
            }
            return spawnInfo.CopyNormalItem();
        }
        public static void RegisterAllNormalItems()
        {
            RegisterAllLuaItemInfo();
            AllItemSpawner.Add(EmptyItem, new NormalItemEmpty());
        }
        private static void RegisterAllLuaItemInfo()
        {
            LuaManager.RegisterAllLuaScriptsOf("Items", (id, path, module) =>
            {
                AllItemSpawner.Add(
                    id,
                    new NormalItemLua(module)
                );
            });
        }
    }
}