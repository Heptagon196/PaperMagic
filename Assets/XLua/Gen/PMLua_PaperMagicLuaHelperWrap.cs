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
    public class PMLuaPaperMagicLuaHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PMLua.PaperMagicLuaHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 6, 6);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnInit", _m_OnInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Log", _m_Log);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RotateVec", _m_RotateVec);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Creature", _g_get_Creature);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Effect", _g_get_Effect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Backpack", _g_get_Backpack);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Player", _g_get_Player);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Projectile", _g_get_Projectile);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Quest", _g_get_Quest);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Creature", _s_set_Creature);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Effect", _s_set_Effect);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Backpack", _s_set_Backpack);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Player", _s_set_Player);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Projectile", _s_set_Projectile);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Quest", _s_set_Quest);
            
			
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
					
					var gen_ret = new PMLua.PaperMagicLuaHelper();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to PMLua.PaperMagicLuaHelper constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Log(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _msg = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.Log( _msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RotateVec(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _vec;translator.Get(L, 2, out _vec);
                    float _angle = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.RotateVec( _vec, _angle );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Creature(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Creature);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Effect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Effect);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Backpack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Backpack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Player(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Player);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Projectile(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Projectile);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Quest(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Quest);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Creature(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Creature = (PMLua.Export.CreatureLua)translator.GetObject(L, 2, typeof(PMLua.Export.CreatureLua));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Effect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Effect = (PMLua.Export.EffectLua)translator.GetObject(L, 2, typeof(PMLua.Export.EffectLua));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Backpack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Backpack = (PMLua.Export.BackpackLua)translator.GetObject(L, 2, typeof(PMLua.Export.BackpackLua));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Player(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Player = (PMLua.Export.PlayerLua)translator.GetObject(L, 2, typeof(PMLua.Export.PlayerLua));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Projectile(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Projectile = (PMLua.Export.ProjectileLua)translator.GetObject(L, 2, typeof(PMLua.Export.ProjectileLua));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Quest(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PMLua.PaperMagicLuaHelper gen_to_be_invoked = (PMLua.PaperMagicLuaHelper)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Quest = (PMLua.Export.QuestLua)translator.GetObject(L, 2, typeof(PMLua.Export.QuestLua));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
