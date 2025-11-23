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
    public class SpellSpellTreeBaseWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Spell.SpellTreeBase);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 10, 9);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEffectInSpellTreePos", _m_GetEffectInSpellTreePos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChildNodeCount", _m_ChildNodeCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnInit", _m_OnInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Execute", _m_Execute);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSize", _m_GetSize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpawnSpellByPath", _m_SpawnSpellByPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMaxNodeCount", _m_GetMaxNodeCount);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxChildNodeCount", _g_get_maxChildNodeCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spellCost", _g_get_spellCost);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spellDamage", _g_get_spellDamage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spellType", _g_get_spellType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spellName", _g_get_spellName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spellDesc", _g_get_spellDesc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "iconPath", _g_get_iconPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Nodes", _g_get_Nodes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Parent", _g_get_Parent);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "id", _s_set_id);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "maxChildNodeCount", _s_set_maxChildNodeCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spellCost", _s_set_spellCost);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spellDamage", _s_set_spellDamage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spellType", _s_set_spellType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spellName", _s_set_spellName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spellDesc", _s_set_spellDesc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "iconPath", _s_set_iconPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Parent", _s_set_Parent);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Spell.SpellTreeBase does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEffectInSpellTreePos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Spell.SpellEffectBase _effect = (Spell.SpellEffectBase)translator.GetObject(L, 2, typeof(Spell.SpellEffectBase));
                    
                        var gen_ret = gen_to_be_invoked.GetEffectInSpellTreePos( _effect );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChildNodeCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.ChildNodeCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
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
            
            
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_GetSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetSize(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.id);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxChildNodeCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.maxChildNodeCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spellCost(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.spellCost);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spellDamage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.spellDamage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spellType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.spellType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spellName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.spellName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spellDesc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.spellDesc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_iconPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.iconPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Nodes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Nodes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Parent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Parent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.id = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_maxChildNodeCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.maxChildNodeCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spellCost(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.spellCost = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spellDamage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.spellDamage = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spellType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                Spell.SpellType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.spellType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spellName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.spellName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spellDesc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.spellDesc = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_iconPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.iconPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Parent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Spell.SpellTreeBase gen_to_be_invoked = (Spell.SpellTreeBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Parent = (Spell.SpellTreeBase)translator.GetObject(L, 2, typeof(Spell.SpellTreeBase));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
