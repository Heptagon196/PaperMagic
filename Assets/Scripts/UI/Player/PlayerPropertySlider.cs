using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Player
{
    public enum SliderPropertyType
    {
        Health,
        Hat,
        Coat,
        Left,
        Right,
        Foot,
    }
    public interface IPropertyPercent
    {
        public float GetPropertyPercent(SliderPropertyType propertyType);
    }
    public class PlayerPropertySlider : MonoBehaviour
    {
        private static readonly Dictionary<SliderPropertyType, IPropertyPercent> PropertyGetters = new();
        public Image slider;
        public SliderPropertyType propertyType;
        private IPropertyPercent _propertyPercent;
        private float _prevValue = -1;
        private void Start()
        {
            _propertyPercent = PropertyGetters[propertyType];
        }
        public static void RegisterProperty(SliderPropertyType propertyType, IPropertyPercent propertyPercent)
        {
            PropertyGetters[propertyType] = propertyPercent;
        }
        private void LateUpdate()
        {
            var value = _propertyPercent.GetPropertyPercent(propertyType);
            if (!Mathf.Approximately(value, _prevValue))
            {
                _prevValue = value;
                slider.fillAmount = value;
            }
        }
    }
}