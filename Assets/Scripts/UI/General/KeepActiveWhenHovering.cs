using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.General
{
    public class KeepActiveWhenHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _isInPointer = false;
        private bool _toDisappear = false;
        public void OnPointerEnter(PointerEventData eventData)
        {
            gameObject.SetActive(true);
            _isInPointer = true;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _isInPointer = false;
            SetToDisappear();
        }
        public void SetToDisappear()
        {
            _toDisappear = true;
        }
        public void OnAppear()
        {
            _toDisappear = false;
        }
        public void Update()
        {
            if (_toDisappear)
            {
                _toDisappear = false;
                if (!_isInPointer)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
