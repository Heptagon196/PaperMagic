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
    public class NPCCreatureBaseWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(NPC.CreatureBase);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTakeDamage", _m_OnTakeDamage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TakeDamage", _m_TakeDamage);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxHealthPoint", _g_get_maxHealthPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "healthPoint", _g_get_healthPoint);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "maxHealthPoint", _s_set_maxHealthPoint);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "healthPoint", _s_set_healthPoint);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "NPC.CreatureBase does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTakeDamage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NPC.CreatureBase gen_to_be_invoked = (NPC.CreatureBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _damage = (float)LuaAPI.lua_tonumber(L, 2);
                    NPC.CreatureBase _source = (NPC.CreatureBase)translator.GetObject(L, 3, typeof(NPC.CreatureBase));
                    
                    gen_to_be_invoked.OnTakeDamage( _damage, _source );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TakeDamage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NPC.CreatureBase gen_to_be_invoked = (NPC.CreatureBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _damage = (float)LuaAPI.lua_tonumber(L, 2);
                    NPC.CreatureBase _source = (NPC.CreatureBase)translator.GetObject(L, 3, typeof(NPC.CreatureBase));
                    
                    gen_to_be_invoked.TakeDamage( _damage, _source );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxHealthPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NPC.CreatureBase gen_to_be_invoked = (NPC.CreatureBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.maxHealthPoint);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_healthPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NPC.CreatureBase gen_to_be_invoked = (NPC.CreatureBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.healthPoint);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_maxHealthPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NPC.CreatureBase gen_to_be_invoked = (NPC.CreatureBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.maxHealthPoint = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_healthPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NPC.CreatureBase gen_to_be_invoked = (NPC.CreatureBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.healthPoint = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
