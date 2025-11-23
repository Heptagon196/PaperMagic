using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.General
{
    public class CheckMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static int _mouseCounter = 0;
        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseCounter++;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseCounter--;
        }
        public static bool MouseOnUI => _mouseCounter > 0;
    }
}