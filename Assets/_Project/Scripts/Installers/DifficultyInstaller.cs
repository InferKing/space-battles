using System.Collections.Generic;
using _Project.Scripts.Model;
using _Project.Scripts.Model.FileManager;
using _Project.Scripts.Model.GameParameters;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class DifficultyInstaller : MonoInstaller
    {
        [SerializeField] private TeamsData _teamsData;
        [SerializeField] private List<MapConfig> _mapConfigs;
    
        public override void InstallBindings()
        {
            Container.BindInstance<IFileManager>(new GameDataManager()).AsSingle();
            Container.BindInstance(_mapConfigs).AsSingle();
            Container.Bind<TeamsData>().FromInstance(_teamsData).AsSingle();
        }
    }
}