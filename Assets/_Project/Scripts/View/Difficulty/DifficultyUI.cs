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

        private void Start()
        {
            _toggle.onValueChanged.AddListener((value) => DifficultyPicked?.Invoke(_difficulty));
        }

        public void Toggle(bool isOn)
        {
            _toggle.SetIsOnWithoutNotify(isOn);
        }
    }
}
