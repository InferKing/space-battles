using _Project.Scripts.Model.FileManager;
using _Project.Scripts.Model.Localization;
using _Project.Scripts.View.Difficulty;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Model.Player
{
    public class PlayerSessionManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
    
        private IFileManager _fileManager;
        private Team _selectedTeam;
        private DifficultyType _difficulty;
        private readonly FieldParameters _fieldParameters = new(
            new Vector2(Constants.OptimalGenerationArea, Constants.OptimalGenerationArea));

        [Inject]
        private void Construct(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        private void OnEnable()
        {
            _uiManager.PlayerReady += OnPlayerReady;
            _uiManager.TeamPicked += OnTeamPicked;
            _uiManager.DifficultyPicked += OnDifficultyPicked;
            _uiManager.StarCountUpdated += OnStarCountUpdated;
            _uiManager.EdgeCountUpdated += OnEdgeCountUpdated;
            _uiManager.TeamRadiusUpdated += OnTeamRadiusUpdated;
            _uiManager.GenerationAreaChanged += OnGenerationAreaChanged;
        }

        private void OnDisable() 
        {
            _uiManager.PlayerReady -= OnPlayerReady;
            _uiManager.TeamPicked -= OnTeamPicked;
            _uiManager.DifficultyPicked -= OnDifficultyPicked;
            _uiManager.StarCountUpdated -= OnStarCountUpdated;
            _uiManager.EdgeCountUpdated -= OnEdgeCountUpdated;
            _uiManager.TeamRadiusUpdated -= OnTeamRadiusUpdated;
            _uiManager.GenerationAreaChanged -= OnGenerationAreaChanged;
        }
        
        private void OnPlayerReady()
        {
            // TODO: переключение сцены
            _fileManager.Save(Constants.SessionFile, new PlayerSession(_fieldParameters, _selectedTeam, _difficulty));
        }

        private void OnGenerationAreaChanged(int area)
        {
            _fieldParameters.SetGenerationArea(area, 
                (isSuccess) => OnValidationResult(isSuccess, LocalizationKeys.ErrorGenerationArea));
        }

        private void OnTeamRadiusUpdated(float radius)
        {
            _fieldParameters.SetTeamCircleRadius(radius, 
                (isSuccess) => OnValidationResult(isSuccess, LocalizationKeys.ErrorTeamRadius));
        }

        private void OnEdgeCountUpdated(int count)
        {
            _fieldParameters.SetDesiredEdgeCount(count, 
                (isSuccess) => OnValidationResult(isSuccess, LocalizationKeys.ErrorEdgeCount));
        }

        private void OnStarCountUpdated(int stars)
        {
            _fieldParameters.SetStarCount(stars, 
                (isSuccess) => OnValidationResult(isSuccess, LocalizationKeys.ErrorStarCount));
        }

        private void OnValidationResult(bool isSuccess, LocalizationKeys localizationKey)
        {
            if (!isSuccess)
            {
                // TODO: добавить локализацию
                _uiManager.ShowErrorMessage($"ОШИБКА {localizationKey}");
            }
        }

        private void OnDifficultyPicked(DifficultyType difficulty)
        {
            Debug.Log(difficulty);
            _difficulty = difficulty;
        }

        private void OnTeamPicked(Team team)
        {
            _selectedTeam = team;
        }
    }
}
