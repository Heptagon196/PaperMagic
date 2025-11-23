#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    
    public class TutorialTestEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.TestEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.TestEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.TestEnum), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", Tutorial.TestEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", Tutorial.TestEnum.E2);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.TestEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialTestEnum(L, (Tutorial.TestEnum)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.TestEnum!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.TestEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class NPCCreatureFactionWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(NPC.CreatureFaction), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(NPC.CreatureFaction), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(NPC.CreatureFaction), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Friendly", NPC.CreatureFaction.Friendly);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Hostile", NPC.CreatureFaction.Hostile);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Neutral", NPC.CreatureFaction.Neutral);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(NPC.CreatureFaction), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushNPCCreatureFaction(L, (NPC.CreatureFaction)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Friendly"))
                {
                    translator.PushNPCCreatureFaction(L, NPC.CreatureFaction.Friendly);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Hostile"))
                {
                    translator.PushNPCCreatureFaction(L, NPC.CreatureFaction.Hostile);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Neutral"))
                {
                    translator.PushNPCCreatureFaction(L, NPC.CreatureFaction.Neutral);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for NPC.CreatureFaction!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for NPC.CreatureFaction! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class NPCCreatureLevelWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(NPC.CreatureLevel), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(NPC.CreatureLevel), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(NPC.CreatureLevel), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", NPC.CreatureLevel.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", NPC.CreatureLevel.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Elite", NPC.CreatureLevel.Elite);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Boss", NPC.CreatureLevel.Boss);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(NPC.CreatureLevel), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushNPCCreatureLevel(L, (NPC.CreatureLevel)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushNPCCreatureLevel(L, NPC.CreatureLevel.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushNPCCreatureLevel(L, NPC.CreatureLevel.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Elite"))
                {
                    translator.PushNPCCreatureLevel(L, NPC.CreatureLevel.Elite);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Boss"))
                {
                    translator.PushNPCCreatureLevel(L, NPC.CreatureLevel.Boss);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for NPC.CreatureLevel!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for NPC.CreatureLevel! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialDerivedClassTestEnumInnerWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E3", Tutorial.DerivedClass.TestEnumInner.E3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E4", Tutorial.DerivedClass.TestEnumInner.E4);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialDerivedClassTestEnumInner(L, (Tutorial.DerivedClass.TestEnumInner)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E3"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E4"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E4);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.DerivedClass.TestEnumInner!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.DerivedClass.TestEnumInner! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}