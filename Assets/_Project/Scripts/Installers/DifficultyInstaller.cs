using _Project.Scripts.Model;
using UnityEngine;
using Zenject;

public class DifficultyInstaller : MonoInstaller
{
    [SerializeField] private TeamsData _teamsData;
    
    public override void InstallBindings()
    {
        Container.Bind<TeamsData>().FromInstance(_teamsData).AsSingle();
    }
}