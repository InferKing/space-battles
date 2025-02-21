using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Difficulty
{
    public class SliderText : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _valueText;

        private void OnEnable()
        {
            UpdateText(_slider.value);
        }

        public void Init(int minValue, int maxValue, int value)
        {
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
            _slider.value = value;
            
            _slider.onValueChanged.AddListener(OnValueChanged);
            UpdateText(_slider.value);
        }

        public void Init(float minValue, float maxValue, float value)
        {
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
            _slider.value = value;
            
            _slider.onValueChanged.AddListener(OnValueChanged);
            UpdateText(_slider.value);
        }
        
        private void OnValueChanged(float value)
        {
            UpdateText(value);
        }

        private void UpdateText(float value)
        {
            _valueText.text = $"{value:N2}";
        }
    }
}