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
    public class SpellSpellTreeBaseLuaWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Spell.SpellTreeBaseLua);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnInit", _m_OnInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Execute", _m_Execute);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpawnSpellByPath", _m_SpawnSpellByPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMaxNodeCount", _m_GetMaxNodeCount);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaScriptPath", _g_get_luaScriptPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "script", _g_get_script);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnCastSpell", _g_get_OnCastSpell);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "luaScriptPath", _s_set_luaScriptPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "script", _s_set_script);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnCastSpell", _s_set_OnCastSpell);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Spell.SpellTreeBaseLua();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Spell.SpellTreeBaseLua constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Execute(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _cost;
                    System.Collections.Generic.List<Spell.SpellEffectBase> _effectList;
                    
                    gen_to_be_invoked.Execute( out _cost, out _effectList );
                    LuaAPI.xlua_pushinteger(L, _cost);
                        
                    translator.Push(L, _effectList);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpawnSpellByPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.SpawnSpellByPath( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMaxNodeCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetMaxNodeCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaScriptPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.luaScriptPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_script(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.script);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnCastSpell(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnCastSpell);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_luaScriptPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.luaScriptPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_script(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.script = (PMLua.LuaScriptExecutor)translator.GetObject(L, 2, typeof(PMLua.LuaScriptExecutor));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnCastSpell(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBaseLua gen_to_be_invoked = (Spell.SpellTreeBaseLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnCastSpell = translator.GetDelegate<Spell.LuaCallOnCast>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
