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
    public class SpellSpellEffectBaseWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Spell.SpellEffectBase);
			Utils.BeginObjectRegister(type, L, translator, 0, 19, 17, 16);
			
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
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Inited", _g_get_Inited);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Applied", _g_get_Applied);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UsePlayerSpawnInfo", _g_get_UsePlayerSpawnInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Source", _g_get_Source);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Owner", _g_get_Owner);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SpawnPosition", _g_get_SpawnPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SpawnTowards", _g_get_SpawnTowards);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BranchPosition", _g_get_BranchPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnApply", _g_get_OnApply);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnUpdate", _g_get_OnUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnTriggerEnter", _g_get_OnTriggerEnter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnTriggerStay", _g_get_OnTriggerStay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnTriggerExit", _g_get_OnTriggerExit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnCollisionEnter", _g_get_OnCollisionEnter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnCollisionStay", _g_get_OnCollisionStay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnCollisionExit", _g_get_OnCollisionExit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnExpired", _g_get_OnExpired);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Inited", _s_set_Inited);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Applied", _s_set_Applied);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UsePlayerSpawnInfo", _s_set_UsePlayerSpawnInfo);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Source", _s_set_Source);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Owner", _s_set_Owner);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SpawnPosition", _s_set_SpawnPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SpawnTowards", _s_set_SpawnTowards);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnApply", _s_set_OnApply);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnUpdate", _s_set_OnUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnTriggerEnter", _s_set_OnTriggerEnter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnTriggerStay", _s_set_OnTriggerStay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnTriggerExit", _s_set_OnTriggerExit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnCollisionEnter", _s_set_OnCollisionEnter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnCollisionStay", _s_set_OnCollisionStay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnCollisionExit", _s_set_OnCollisionExit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnExpired", _s_set_OnExpired);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Spell.SpellEffectBase does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpawnEffectByPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.TriggerOnExpired(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Inited(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Inited);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Applied(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Applied);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UsePlayerSpawnInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UsePlayerSpawnInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Source(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Source);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Owner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Owner);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SpawnPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.SpawnPosition);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SpawnTowards(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.SpawnTowards);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BranchPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.BranchPosition);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnApply(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnApply);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnTriggerEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnTriggerEnter);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnTriggerStay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnTriggerStay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnTriggerExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnTriggerExit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnCollisionEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnCollisionEnter);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnCollisionStay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnCollisionStay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnCollisionExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnCollisionExit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnExpired(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnExpired);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Inited(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Inited = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Applied(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Applied = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UsePlayerSpawnInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UsePlayerSpawnInfo = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Source(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Source = (NPC.CreatureBase)translator.GetObject(L, 2, typeof(NPC.CreatureBase));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Owner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Owner = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SpawnPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.SpawnPosition = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SpawnTowards(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.SpawnTowards = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnApply(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnApply = (System.Collections.Generic.List<Spell.PatchOnApplyEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnApplyEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnUpdate = (System.Collections.Generic.List<Spell.PatchOnUpdateEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnUpdateEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnTriggerEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnTriggerEnter = (System.Collections.Generic.List<Spell.PatchOnTriggerEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnTriggerEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnTriggerStay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnTriggerStay = (System.Collections.Generic.List<Spell.PatchOnTriggerEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnTriggerEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnTriggerExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnTriggerExit = (System.Collections.Generic.List<Spell.PatchOnTriggerEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnTriggerEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnCollisionEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnCollisionEnter = (System.Collections.Generic.List<Spell.PatchOnCollisionEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnCollisionEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnCollisionStay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnCollisionStay = (System.Collections.Generic.List<Spell.PatchOnCollisionEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnCollisionEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnCollisionExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnCollisionExit = (System.Collections.Generic.List<Spell.PatchOnCollisionEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnCollisionEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnExpired(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellEffectBase gen_to_be_invoked = (Spell.SpellEffectBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnExpired = (System.Collections.Generic.List<Spell.PatchOnExpiredEvent>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Spell.PatchOnExpiredEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
