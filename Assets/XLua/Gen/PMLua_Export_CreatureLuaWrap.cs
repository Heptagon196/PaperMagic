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
    public class PMLuaExportCreatureLuaWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PMLua.Export.CreatureLua);
			Utils.BeginObjectRegister(type, L, translator, 0, 11, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCreatureLevel", _m_GetCreatureLevel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCreatureFaction", _m_GetCreatureFaction);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCreatureInfo", _m_GetCreatureInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoDamage", _m_DoDamage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Spawn", _m_Spawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPersistent", _m_SetPersistent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CastSpell", _m_CastSpell);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveTowards", _m_MoveTowards);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CanSeeTarget", _m_CanSeeTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAnimation", _m_SetAnimation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CastSpellTree", _m_CastSpellTree);
			
			
			
			
			
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
					
					var gen_ret = new PMLua.Export.CreatureLua();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to PMLua.Export.CreatureLua constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCreatureLevel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _obj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.GetCreatureLevel( _obj );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCreatureFaction(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _obj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.GetCreatureFaction( _obj );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCreatureInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _obj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.GetCreatureInfo( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoDamage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _source = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    float _damage = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DoDamage( _source, _target, _damage );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Spawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _id = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Vector3 _spawnPosition;translator.Get(L, 3, out _spawnPosition);
                    
                        var gen_ret = gen_to_be_invoked.Spawn( _id, _spawnPosition );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPersistent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _creature = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.SetPersistent( _creature );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CastSpell(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _source = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    string _spellID = LuaAPI.lua_tostring(L, 3);
                    UnityEngine.Vector3 _spawnPosition;translator.Get(L, 4, out _spawnPosition);
                    UnityEngine.Vector3 _spawnTowards;translator.Get(L, 5, out _spawnTowards);
                    
                        var gen_ret = gen_to_be_invoked.CastSpell( _source, _spellID, _spawnPosition, _spawnTowards );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveTowards(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _gameObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    UnityEngine.Vector3 _target;translator.Get(L, 3, out _target);
                    float _speed = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.MoveTowards( _gameObject, _target, _speed );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CanSeeTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& translator.Assignable<UnityEngine.GameObject>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.GameObject _owner = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    float _maxDistance = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.CanSeeTarget( _owner, _target, _maxDistance );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& translator.Assignable<UnityEngine.GameObject>(L, 3)) 
                {
                    UnityEngine.GameObject _owner = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.CanSeeTarget( _owner, _target );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to PMLua.Export.CreatureLua.CanSeeTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAnimation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _owner = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    string _stage = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SetAnimation( _owner, _stage );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CastSpellTree(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.CreatureLua gen_to_be_invoked = (PMLua.Export.CreatureLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _source = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    System.Collections.Generic.List<System.Collections.Generic.List<string>> _spellID = (System.Collections.Generic.List<System.Collections.Generic.List<string>>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<System.Collections.Generic.List<string>>));
                    UnityEngine.Vector3 _spawnPosition;translator.Get(L, 4, out _spawnPosition);
                    UnityEngine.Vector3 _spawnTowards;translator.Get(L, 5, out _spawnTowards);
                    
                        var gen_ret = gen_to_be_invoked.CastSpellTree( _source, _spellID, _spawnPosition, _spawnTowards );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
