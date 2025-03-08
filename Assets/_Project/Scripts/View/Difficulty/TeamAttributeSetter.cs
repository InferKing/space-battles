using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Difficulty
{
    [System.Serializable]
    public class TeamAttributeSetter
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _value;

        private const float AnimationTime = 1.2f;
        
        public void SetData(float min, float max, float value, float delayTime, bool isRound = true)
        {
            _slider.minValue = min;
            _slider.maxValue = max;
            _value.text = isRound ? $"{value:N0}" : $"{value:N2}";
            
            PlayAnimationFill(value, delayTime);
        }

        public void SetData(int min, int max, int value, float delayTime)
        {
            _slider.minValue = min;
            _slider.maxValue = max;
            _value.text = value.ToString();

            PlayAnimationFill(value, delayTime);
        }

        private void PlayAnimationFill(float value, float delayTime)
        {
            _slider.DOValue(value, AnimationTime).SetEase(Ease.OutBack).SetDelay(delayTime);
        }
    }
}