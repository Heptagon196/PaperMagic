using System.Collections;
using PMLua;
using XLua;

namespace UI.ChatBox
{
    public interface IChatBoxProvider
    {
        public bool IsRunning { get; }
        public IEnumerator StartChat(string filePath);
        public void CloseChat();
    }
}