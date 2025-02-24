using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Difficulty
{
    public class ToggleUI : MonoBehaviour
    {
        [SerializeField] private int _type;
        [SerializeField] private Toggle _toggle;

        public event Action<int> Picked;

        public int Type => _type;

        private void Start()
        {
            _toggle.onValueChanged.AddListener((value) => Picked?.Invoke(_type));
        }

        public void Toggle(bool isOn)
        {
            _toggle.SetIsOnWithoutNotify(isOn);
        }
    }
}