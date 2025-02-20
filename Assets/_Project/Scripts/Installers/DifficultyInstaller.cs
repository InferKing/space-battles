using _Project.Scripts.Model;
using _Project.Scripts.Model.FileManager;
using UnityEngine;
using Zenject;

public class DifficultyInstaller : MonoInstaller
{
    [SerializeField] private TeamsData _teamsData;
    
    public override void InstallBindings()
    {
        Container.BindInstance<IFileManager>(new GameDataManager()).AsSingle();
        Container.Bind<TeamsData>().FromInstance(_teamsData).AsSingle();
    }
}