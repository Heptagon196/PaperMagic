using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace PMLua
{
    public static class LuaExtensions
    {
        public static T TryGet<T>(this LuaTable table, string key, T value)
        {
            return table.ContainsKey(key) ? table.Get<T>(key) : value;
        }
    }
    public class LuaManager : MonoBehaviour
    {
        public const string ScriptRootPath = "GameData/Scripts/";
        private static LuaManager _instance = null;
        public static LuaManager Instance => _instance;
        public static readonly LuaEnv Env = new(); //all lua behaviour shared one luaenv only!
        private static float _lastGCTime = 0;
        private const float GCInterval = 1; //1 second
        public PaperMagicLuaHelper PaperMagicLuaHelper;
        private readonly Queue<LuaScriptExecutor> _toDispose = new();
        public string GetLoadPath(string path)
        {
            if (!path.StartsWith(ScriptRootPath))
            {
                return ScriptRootPath + path;
            }

            return path;
        }
        public byte[] CustomLoader(ref string path)
        {
            var loadPath = GetLoadPath(path);
            try
            {
                using var reader = new StreamReader(loadPath);
                var content = reader.ReadToEnd();
                return System.Text.Encoding.UTF8.GetBytes(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                Env.AddLoader(CustomLoader);
                PaperMagicLuaHelper = new PaperMagicLuaHelper();
                PaperMagicLuaHelper.OnInit();
                return;
            }
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        public void ToDestroyScript(LuaScriptExecutor scriptExecutor)
        {
            _toDispose.Enqueue(scriptExecutor);
        }
        public void Update()
        {
            if (Time.time - _lastGCTime > GCInterval)
            {
                Env.Tick();
                _lastGCTime = Time.time;
            }
            while (_toDispose.Count > 0)
            {
                var dispose = _toDispose.Dequeue();
                dispose.DestroyScript();
            }
        }
        public delegate void OnFindLuaScript(string id, string path, LuaTable module);
        public static void RegisterAllLuaScriptsOf(string directory, OnFindLuaScript onFindFile)
        {
            var files = Directory.GetFiles(LuaManager.ScriptRootPath + directory, "*.lua", SearchOption.AllDirectories);
            foreach (var filePath in files)
            {
                var path = filePath.Replace('\\', '/');
                var script = new LuaScriptExecutor
                {
                    luaScriptPath = path
                };
                script.InitScriptEnv();
                var module = script.RunScript(false);
                var moduleID = module?.Get<string>("ID");
                if (moduleID != null)
                {
                    onFindFile(moduleID, path, module);
                }
            }
        }
    }
}