using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    public class ConfirmBox : MonoBehaviour
    {
        public Text msgText;
        public GameObject root;
        public Button confirmButton;
        public Button cancelButton;
        private Action _onConfirm, _onCancel;
        private Image _mask;
        private void Start()
        {
            _mask = GetComponent<Image>();
            root.SetActive(false);
            _mask.enabled = false;
            confirmButton.onClick.AddListener(OnConfirm);
            cancelButton.onClick.AddListener(OnCancel);
        }
        public void ShowConfirmBox(string msg, Action onConfirm, Action onCancel)
        {
            _mask.enabled = true;
            msgText.text = msg;
            _onConfirm = onConfirm;
            _onCancel = onCancel;
            root.SetActive(true);
        }
        private void OnConfirm()
        {
            _mask.enabled = false;
            _onConfirm?.Invoke();
            _onConfirm = null;
            _onCancel = null;
            root.SetActive(false);
        }
        private void OnCancel()
        {
            _mask.enabled = false;
            _onCancel?.Invoke();
            _onConfirm = null;
            _onCancel = null;
            root.SetActive(false);
        }
    }
}