using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Model.Field;
using _Project.Scripts.Model.FileManager;
using _Project.Scripts.Model.GameParameters;
using _Project.Scripts.View.Difficulty;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.Model.Player
{
    public class PlayerSessionManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
    
        private FieldParameters _fieldParameters;
        private Team _selectedTeam;
        private DifficultyType _difficulty;

        private IFileManager _fileManager;
        private List<MapConfig> _mapConfigs; 
        
        [Inject]
        private void Construct(IFileManager fileManager, List<MapConfig> mapConfigs)
        {
            _fileManager = fileManager;
            _mapConfigs = mapConfigs;
        }

        private void OnEnable()
        {
            _uiManager.PlayerReady += OnPlayerReady;
            _uiManager.TeamPicked += OnTeamPicked;
            _uiManager.DifficultyPicked += OnDifficultyPicked;
            _uiManager.MapPicked += OnMapPicked;
            _uiManager.EdgeAmountPicked += OnEdgeAmountPicked;
        }

        private void Start()
        {
            _fieldParameters = new FieldParameters(_mapConfigs.First(map => map.Map == Constants.OptimalMapSize),
                Constants.OptimalEdgeAmount);
        }

        private void OnDisable() 
        {
            _uiManager.PlayerReady -= OnPlayerReady;
            _uiManager.TeamPicked -= OnTeamPicked;
            _uiManager.DifficultyPicked -= OnDifficultyPicked;
            _uiManager.MapPicked -= OnMapPicked;
            _uiManager.EdgeAmountPicked -= OnEdgeAmountPicked;
        }

        private void OnEdgeAmountPicked(int pickedAmountType)
        {
            _fieldParameters.EdgeAmountType = (EdgeAmountType)pickedAmountType;
        }

        private void OnMapPicked(int pickedMapType)
        {
            var mapType = (MapType)pickedMapType;

            var config = _mapConfigs.First(map => map.Map == mapType);
            _fieldParameters.MapConfig = config;
        }

        private void OnDifficultyPicked(int difficulty)
        {
            _difficulty = (DifficultyType)difficulty;
        }
        
        private void OnPlayerReady()
        {
            // TODO: переключение сцены
            _fileManager.Save(Constants.SessionFile, new PlayerSession(_fieldParameters, _selectedTeam, _difficulty));
            SceneManager.LoadScene("Main");
        }

        private void OnTeamPicked(Team team)
        {
            _selectedTeam = team;
        }
    }
}
