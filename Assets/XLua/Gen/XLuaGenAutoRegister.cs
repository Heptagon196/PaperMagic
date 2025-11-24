#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass), TutorialDerivedClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.ICalc), TutorialICalcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClassExtensions), TutorialDerivedClassExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.ILuaChatLine), UIChatBoxILuaChatLineWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.LuaChatText), UIChatBoxLuaChatTextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.LuaChatOptions), UIChatBoxLuaChatOptionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.LuaChatAction), UIChatBoxLuaChatActionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.LuaChatClose), UIChatBoxLuaChatCloseWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.LuaChatCreator), UIChatBoxLuaChatCreatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.LuaChatShopItem), UIChatBoxLuaChatShopItemWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UI.ChatBox.LuaChatOpenShop), UIChatBoxLuaChatOpenShopWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Spell.SpellEffectBase), SpellSpellEffectBaseWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Spell.SpellEffectLua), SpellSpellEffectLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Spell.SpellTreeBase), SpellSpellTreeBaseWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Spell.SpellTreeBaseLua), SpellSpellTreeBaseLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.LuaScriptExecutor), PMLuaLuaScriptExecutorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.PaperMagicLuaHelper), PMLuaPaperMagicLuaHelperWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.Export.BackpackLua), PMLuaExportBackpackLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.Export.ChatLua), PMLuaExportChatLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.Export.CreatureLua), PMLuaExportCreatureLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.Export.EffectLua), PMLuaExportEffectLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.Export.PlayerLua), PMLuaExportPlayerLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.Export.ProjectileLua), PMLuaExportProjectileLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(PMLua.Export.QuestLua), PMLuaExportQuestLuaWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(NPC.CreatureFaction), NPCCreatureFactionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(NPC.CreatureBase), NPCCreatureBaseWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(NPC.CreatureLevel), NPCCreatureLevelWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(NPC.CreatureInfoBase), NPCCreatureInfoBaseWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass.TestEnumInner), TutorialDerivedClassTestEnumInnerWrap.__Register);
        
        
        
        }
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(UI.ChatBox.ILuaChatLine), UIChatBoxILuaChatLineBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(NPC.ICreatureAnimLua), NPCICreatureAnimLuaBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Tutorial.CSCallLua.ItfD), TutorialCSCallLuaItfDBridge.__Create);
            
        }
        
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter(Init);
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
