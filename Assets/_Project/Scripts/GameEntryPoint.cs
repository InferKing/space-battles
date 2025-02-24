using _Project.Scripts.Model;
using _Project.Scripts.Model.Core;
using _Project.Scripts.Model.FileManager;
using _Project.Scripts.Model.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{   
    public class GameEntryPoint : MonoInstaller
    {
        [SerializeField] private Star _starPrefab;
        [SerializeField] private Ship _shipPrefab;
        
        public override void InstallBindings()
        {
            var fileManager = new GameDataManager();
            var playerSession = fileManager.Load<PlayerSession>(Constants.SessionFile);
            var fieldParameters = playerSession.FieldParameters;

            Container.BindInstance(fieldParameters).AsSingle();
            
            Container.BindFactory<Star, Star.Factory>().FromComponentInNewPrefab(_starPrefab);
            Container.BindFactory<Team, Ship, Ship.Factory>().FromComponentInNewPrefab(_shipPrefab);
        }
    }
}