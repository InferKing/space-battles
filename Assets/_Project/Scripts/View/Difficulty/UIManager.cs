using System;
using _Project.Scripts.Model;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.View.Difficulty
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIWindow _teamPanel;
        [SerializeField] private UIWindow _settingsPanel;

        [Header("Settings UI for the game session")] 
        [SerializeField] private PickerUI _difficulty;
        [SerializeField] private PickerUI _mapSize;
        [SerializeField] private PickerUI _edgeAmount;
        
        public event Action<Team> TeamPicked;
        public event Action<int> DifficultyPicked;
        public event Action<int> MapPicked;
        public event Action<int> EdgeAmountPicked;
        public event Action PlayerReady;
        
        private Team _team;

        private void OnEnable()
        {
            _difficulty.Init((int)Constants.OptimalDifficulty);
            _mapSize.Init((int)Constants.OptimalMapSize);
            _edgeAmount.Init((int)Constants.OptimalEdgeAmount);
    
            _difficulty.Picked += OnDifficultyPicked;
            _mapSize.Picked += OnMapPicked;
            _edgeAmount.Picked += OnEdgeAmountPicked;
        }

        private void OnDisable()
        {
            _difficulty.Picked -= OnDifficultyPicked;
            _mapSize.Picked -= OnMapPicked;
            _edgeAmount.Picked -= OnEdgeAmountPicked;
        }

        public void PickTeam(int team)
        {
            TeamPicked?.Invoke((Team)team);

            ShowNextWindow();
        }

        public void ShowPrevious()
        {
            var sequence = DOTween.Sequence();
            
            sequence.Append(_settingsPanel.Hide().OnComplete(() =>
            {
                _settingsPanel.gameObject.SetActive(false);
                _teamPanel.gameObject.SetActive(true);
            }));
            sequence.Append(_teamPanel.Show());
        }

        public void StartGame()
        {
            PlayerReady?.Invoke();
        }
        
        private void OnDifficultyPicked(int value) => DifficultyPicked?.Invoke(value);
        private void OnMapPicked(int value) => MapPicked?.Invoke(value);
        private void OnEdgeAmountPicked(int value) => EdgeAmountPicked?.Invoke(value);
        
        private void ShowNextWindow()
        {
            var sequence = DOTween.Sequence();
            
            sequence.Append(_teamPanel.Hide().OnComplete(() =>
            {
                _teamPanel.gameObject.SetActive(false);
                _settingsPanel.gameObject.SetActive(true);
            }));
            sequence.Append(_settingsPanel.Show());
        }
    }
}