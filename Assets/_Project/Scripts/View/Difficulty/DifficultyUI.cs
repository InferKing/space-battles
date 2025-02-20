using System;
using _Project.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Difficulty
{
    public class DifficultyUI : MonoBehaviour
    {
        [SerializeField] private DifficultyType _difficulty;
        [SerializeField] private Toggle _toggle;

        public event Action<DifficultyType> DifficultyPicked;

        public DifficultyType Type => _difficulty;
        
        public void Toggle(bool isOn)
        {
            _toggle.isOn = isOn;
        }

        public void Pick()
        {
            DifficultyPicked?.Invoke(_difficulty);
        }
    }
}
