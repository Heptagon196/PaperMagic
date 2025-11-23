using System;
using System.IO;
using XLua;

namespace PMLua
{
    [CSharpCallLua]
    public delegate void LuaCallOnStart(LuaTable self);

    [LuaCallCSharp, Serializable]
    public class LuaScriptExecutor
    {
        public string luaScriptPath;
        public LuaTable Scope;
        public static int Counter = 0;
        protected static LuaEnv Env => LuaManager.Env;

        public void InitScriptEnv()
        {
        }
        public LuaTable RunScript(bool callOnStart = true)
        {
            // 执行脚本
            object[] ret = null;
            var loadPath = LuaManager.Instance.GetLoadPath(luaScriptPath);
            string content = "";
            try
            {
                using var reader = new StreamReader(loadPath);
                content = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            ret = Env.DoString(content, loadPath);
            // 从 Lua 脚本域中获取定义的函数
            var table = ret?.Length > 0 ? ret[0] as LuaTable : null;
            if (callOnStart)
            {
                var luaStart = table?.Get<LuaCallOnStart>("OnStart");
                luaStart?.Invoke(table);
            }
            return table;
        }
        public void DestroyScript()
        {
            if (Scope != null)
            {
                Scope.Dispose();
            }
        }
        ~LuaScriptExecutor()
        {
            DestroyScript();
        }
    }
}