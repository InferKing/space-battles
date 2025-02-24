using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.View.Difficulty
{
    public class PickerUI : MonoBehaviour
    {
        [SerializeField] private List<ToggleUI> _toggles;

        public event Action<int> Picked;

        private int _defaultType;

        public void Init(int defaultValue)
        {
            _defaultType = defaultValue;
            
            _toggles.ForEach(toggle =>
            {
                toggle.Picked += OnPicked;
                toggle.Toggle(IsDefaultDifficulty(toggle, _defaultType));
            });
        }

        private bool IsDefaultDifficulty(ToggleUI toggle, int defaultType)
        {
            return toggle.Type == defaultType;
        }

        private void OnPicked(int type)
        {
            _toggles.ForEach(toggle => toggle.Toggle(toggle.Type == type));
            Picked?.Invoke(type);
        }

        private void OnDisable()
        {
            _toggles.ForEach(toggle => toggle.Picked -= OnPicked);
        }
    }
}
