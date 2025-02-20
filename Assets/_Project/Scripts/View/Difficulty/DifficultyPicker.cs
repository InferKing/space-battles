using System;
using System.Collections.Generic;
using _Project.Scripts.Model;
using UnityEngine;

namespace _Project.Scripts.View.Difficulty
{
    public class DifficultyPicker : MonoBehaviour
    {
        [SerializeField] private List<DifficultyUI> _toggles;

        public event Action<DifficultyType> DifficultyPicked; 
        
        private void Start()
        {
            _toggles.ForEach(toggle =>
            {
                toggle.DifficultyPicked += OnDifficultyPicked;
                toggle.Toggle(IsDefaultDifficulty(toggle));
            });
        }

        private bool IsDefaultDifficulty(DifficultyUI toggle)
        {
            return toggle.Type == Constants.OptimalDifficulty;
        }

        private void OnDifficultyPicked(DifficultyType difficulty)
        {
            _toggles.ForEach(toggle => toggle.Toggle(toggle.Type == difficulty));
            DifficultyPicked?.Invoke(difficulty);
        }

        private void OnDisable()
        {
            _toggles.ForEach(toggle => toggle.DifficultyPicked -= OnDifficultyPicked);
        }
    }
}
