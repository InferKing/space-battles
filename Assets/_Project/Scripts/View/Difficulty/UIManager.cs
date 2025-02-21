using System;
using _Project.Scripts.Model;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Difficulty
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIWindow _teamPanel;
        [SerializeField] private UIWindow _settingsPanel;

        // TODO: слайдеры должны переехать в UIParameters
        [Header("Settings UI for the game session")] 
        [SerializeField] private DifficultyPicker _difficultyPicker;
        [SerializeField] private Slider _starCount;
        [SerializeField] private Slider _teamRadius;
        [SerializeField] private Slider _edgeCount;
        [SerializeField] private Slider _generationArea;

        public event Action<Team> TeamPicked;
        public event Action PlayerReady;

        public event Action<DifficultyType> DifficultyPicked;
        public event Action<int> StarCountUpdated;
        public event Action<float> TeamRadiusUpdated;
        public event Action<int> EdgeCountUpdated;
        public event Action<int> GenerationAreaChanged;
        
        private Team _team;

        private void Start()
        {
            _difficultyPicker.DifficultyPicked += DifficultyPicked;
            _starCount.onValueChanged.AddListener(value => StarCountUpdated?.Invoke((int)value));
            _teamRadius.onValueChanged.AddListener(value => TeamRadiusUpdated?.Invoke(value));
            _edgeCount.onValueChanged.AddListener(value => EdgeCountUpdated?.Invoke((int)value));
            _generationArea.onValueChanged.AddListener(value => GenerationAreaChanged?.Invoke((int)value));
        }

        private void OnDisable()
        {
            _difficultyPicker.DifficultyPicked -= DifficultyPicked;
        }

        public void PickTeam(int team)
        {
            TeamPicked?.Invoke((Team)team);

            ShowNextWindow();
        }

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

        public void ShowErrorMessage(string message)
        {
            Debug.LogError(message);
        }
    }
}