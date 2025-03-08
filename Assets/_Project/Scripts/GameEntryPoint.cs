using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Boids;
using _Project.Scripts.Configs;
using _Project.Scripts.Controller;
using _Project.Scripts.Controller.CameraSystem;
using _Project.Scripts.Model;
using _Project.Scripts.Model.Core;
using _Project.Scripts.Model.Core.TargetSelector;
using _Project.Scripts.Model.Field;
using _Project.Scripts.Model.FileManager;
using _Project.Scripts.Model.Player;
using _Project.Scripts.Model.StarSystem;
using _Project.Scripts.Model.Upgrades;
using _Project.Scripts.Model.XPSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{   
    public class GameEntryPoint : MonoInstaller
    {
        [SerializeField] private Star _starPrefab;
        [SerializeField] private Ship _shipPrefab;
        [SerializeField] private List<PlayerData> _playerData;
        [SerializeField] private FieldGenerator _fieldGenerator;
        
        public override void InstallBindings()
        {
            var upgrades = new List<Upgrade>
            {
                new DamageUpgrade(UpgradeType.Damage, 5f),
                new AgentHealthUpgrade(UpgradeType.AgentHealth, 5f),
                new AgentSpawnUpgrade(UpgradeType.AgentSpawn, 2),
                new AttackSpeedUpgrade(UpgradeType.AttackSpeed, 0.25f, 0.2f),
                new MoveSpeedUpgrade(UpgradeType.MoveSpeed, 0.3f, 0.25f),
                new PlanetHealthUpgrade(UpgradeType.PlanetHealth, 500)
            };
            
            var fileManager = new GameDataManager();
            var playerSession = fileManager.Load<PlayerSession>(Constants.SessionFile);
            var fieldParameters = playerSession.FieldParameters;
            var inputController = new InputController();
            var playerSelector = new PlayerSelector(inputController);
            
            Container.BindInstance(inputController).AsSingle();
            Container.Bind<ITickable>().To<InputController>().FromInstance(inputController);
            Container.BindInterfacesTo<PlayerSelector>().FromInstance(playerSelector).AsSingle();
            
            var players = _playerData.Select(data =>
            {
                var xpLevelSystem = new XpLevelSystem();
                return new Player(data, new UpgradeManager(upgrades, xpLevelSystem), xpLevelSystem,
                    data.Team == playerSession.Team 
                        ? playerSelector
                        : new BotSelector());
            }).ToList();

            var player = players.First(player => player.Team == playerSession.Team);

            Container.BindInstance(player).AsSingle();
            Container.BindInstance(players).AsSingle();
            Container.BindInstance(upgrades).AsSingle();
            Container.BindInstance(fieldParameters).AsSingle();
            Container.BindInterfacesTo<CameraController>().AsSingle();
            Container.Bind<FieldGenerator>().FromComponentInNewPrefab(_fieldGenerator).AsSingle();
            Container.BindInterfacesAndSelfTo<BoidManager>().AsSingle();

            Container.BindInstance<IFileManager>(fileManager).AsSingle();

            Container.BindFactory<Star, Star.Factory>().FromComponentInNewPrefab(_starPrefab);
            Container.BindFactory<Team, Player, Ship, Ship.Factory>().FromComponentInNewPrefab(_shipPrefab);
        }
    }
}