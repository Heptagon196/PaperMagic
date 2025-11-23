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
    public class SpellSpellEffectLuaWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Spell.SpellEffectLua);
			Utils.BeginObjectRegister(type, L, translator, 0, 19, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpawnEffectByPath", _m_SpawnEffectByPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFloat", _m_SetFloat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetFloat", _m_GetFloat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetString", _m_SetString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetString", _m_GetString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetObject", _m_SetObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetObject", _m_GetObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ContainsData", _m_ContainsData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetID", _m_GetID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnInit", _m_OnInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ApplyEffect", _m_ApplyEffect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnUpdate", _m_TriggerOnUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnTriggerEnter", _m_TriggerOnTriggerEnter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnTriggerStay", _m_TriggerOnTriggerStay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnTriggerExit", _m_TriggerOnTriggerExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnCollisionEnter", _m_TriggerOnCollisionEnter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnCollisionStay", _m_TriggerOnCollisionStay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnCollisionExit", _m_TriggerOnCollisionExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerOnExpired", _m_TriggerOnExpired);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaScriptPath", _g_get_LuaScriptPath);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "LuaScriptPath", _s_set_LuaScriptPath);
            
			
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
					
					var gen_ret = new Spell.SpellEffectLua();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Spell.SpellEffectLua constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpawnEffectByPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.SpawnEffectByPath( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFloat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    float _value = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetFloat( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFloat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetFloat( _key );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    string _value = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SetString( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetString( _key );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    object _value = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.SetObject( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetObject( _key );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ContainsData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.ContainsData( _key );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetID(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplyEffect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _spawnLocation;translator.Get(L, 2, out _spawnLocation);
                    UnityEngine.Vector3 _spawnTowards;translator.Get(L, 3, out _spawnTowards);
                    
                    gen_to_be_invoked.ApplyEffect( _spawnLocation, _spawnTowards );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.TriggerOnUpdate( _deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnTriggerEnter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collider _other = (UnityEngine.Collider)translator.GetObject(L, 2, typeof(UnityEngine.Collider));
                    
                    gen_to_be_invoked.TriggerOnTriggerEnter( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnTriggerStay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collider _other = (UnityEngine.Collider)translator.GetObject(L, 2, typeof(UnityEngine.Collider));
                    
                    gen_to_be_invoked.TriggerOnTriggerStay( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnTriggerExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collider _other = (UnityEngine.Collider)translator.GetObject(L, 2, typeof(UnityEngine.Collider));
                    
                    gen_to_be_invoked.TriggerOnTriggerExit( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnCollisionEnter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collision _other = (UnityEngine.Collision)translator.GetObject(L, 2, typeof(UnityEngine.Collision));
                    
                    gen_to_be_invoked.TriggerOnCollisionEnter( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnCollisionStay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collision _other = (UnityEngine.Collision)translator.GetObject(L, 2, typeof(UnityEngine.Collision));
                    
                    gen_to_be_invoked.TriggerOnCollisionStay( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnCollisionExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collision _other = (UnityEngine.Collision)translator.GetObject(L, 2, typeof(UnityEngine.Collision));
                    
                    gen_to_be_invoked.TriggerOnCollisionExit( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerOnExpired(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.TriggerOnExpired(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaScriptPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.LuaScriptPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaScriptPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectLua gen_to_be_invoked = (Spell.SpellEffectLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LuaScriptPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
