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
    public class UIChatBoxLuaChatShopItemWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UI.ChatBox.LuaChatShopItem);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 7, 7);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "SellSlot", _g_get_SellSlot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SellID", _g_get_SellID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SellCount", _g_get_SellCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BuySlot", _g_get_BuySlot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BuyID", _g_get_BuyID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BuyCount", _g_get_BuyCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BuyLimit", _g_get_BuyLimit);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "SellSlot", _s_set_SellSlot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SellID", _s_set_SellID);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SellCount", _s_set_SellCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BuySlot", _s_set_BuySlot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BuyID", _s_set_BuyID);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BuyCount", _s_set_BuyCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BuyLimit", _s_set_BuyLimit);
            
			
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
					
					var gen_ret = new UI.ChatBox.LuaChatShopItem();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UI.ChatBox.LuaChatShopItem constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SellSlot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.SellSlot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SellID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.SellID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SellCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.SellCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BuySlot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.BuySlot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BuyID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.BuyID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BuyCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.BuyCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BuyLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.BuyLimit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SellSlot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SellSlot = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SellID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SellID = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SellCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SellCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BuySlot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BuySlot = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BuyID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BuyID = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BuyCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BuyCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BuyLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UI.ChatBox.LuaChatShopItem gen_to_be_invoked = (UI.ChatBox.LuaChatShopItem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BuyLimit = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
