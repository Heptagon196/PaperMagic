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
    public class PMLuaExportBackpackLuaWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PMLua.Export.BackpackLua);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetNum", _m_SetNum);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddNum", _m_AddNum);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNum", _m_GetNum);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEquipped", _m_GetEquipped);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Equip", _m_Equip);
			
			
			
			
			
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
					
					var gen_ret = new PMLua.Export.BackpackLua();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to PMLua.Export.BackpackLua constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetNum(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.BackpackLua gen_to_be_invoked = (PMLua.Export.BackpackLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _slot = LuaAPI.xlua_tointeger(L, 2);
                    string _id = LuaAPI.lua_tostring(L, 3);
                    int _num = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.SetNum( _slot, _id, _num );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddNum(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.BackpackLua gen_to_be_invoked = (PMLua.Export.BackpackLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _slot = LuaAPI.xlua_tointeger(L, 2);
                    string _id = LuaAPI.lua_tostring(L, 3);
                    int _num = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.AddNum( _slot, _id, _num );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNum(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.BackpackLua gen_to_be_invoked = (PMLua.Export.BackpackLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _slot = LuaAPI.xlua_tointeger(L, 2);
                    string _id = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.GetNum( _slot, _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEquipped(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.BackpackLua gen_to_be_invoked = (PMLua.Export.BackpackLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _slot = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetEquipped( _slot );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equip(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PMLua.Export.BackpackLua gen_to_be_invoked = (PMLua.Export.BackpackLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _slot = LuaAPI.xlua_tointeger(L, 2);
                    string _equipID = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.Equip( _slot, _equipID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
