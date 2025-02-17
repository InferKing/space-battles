using _Project.Scripts.Model;
using _Project.Scripts.Model.FileManager;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Difficulty
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _teamPanel;
        [SerializeField] private GameObject _settingsPanel;
        
        private IFileManager _fileManager;
        private Team _team;
        
        [Inject]
        private void Construct(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        
        public void PickTeam(Team team)
        {
            // TODO: вычленить сохранение команды куда-то, должен только вызывать событие выбора команды
            _team = team;
        }

        public void ShowNextUI()
        {
            // TODO: Убрать GameObject, использовать класс что-то типа UIWindow.
            // TODO: Реализовать логику переключения окон с анимациями
        }

        public void ShowPrevious()
        {
            
        }
    }
}