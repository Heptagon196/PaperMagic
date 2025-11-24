using System;
using UI.ChatBox;
using UnityEngine;
using XLua;

namespace PMLua.Export
{
    [LuaCallCSharp]
    public class ChatLua
    {
        public void StartChat(string filePath)
        {
            ChatBox.Instance.RunScript(filePath);
        }
        public void TryCloseChat(string filePath)
        {
            if (IsRunningChat(filePath))
            {
                ChatBox.Instance.CloseChat();
            }
        }
        public bool IsRunningAnyChat()
        {
            return ChatBox.Instance.IsRunningChat();
        }
        public bool IsRunningChat(string scriptPath)
        {
            return ChatBox.Instance.IsRunningChat(scriptPath);
        }
        public void SetInteractable(string moduleName, bool interactable, Action callBack)
        {
            ChatBox.Instance.SetInteractable(moduleName, interactable, callBack);
        }
    }
}