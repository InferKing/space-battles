using _Project.Scripts.Model.Core;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Model
{
    public class SomeShit
    {
        public string name;
        
        public SomeShit(string name)
        {
            this.name = name;
        }
    }
    
    public class GameEntryPoint : MonoInstaller
    {
        [SerializeField] private Star _starPrefab;
        [SerializeField] private Ship _shipPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInstance(new SomeShit("Здарова")).AsSingle();
            Container.BindFactory<Star, Star.Factory>().FromComponentInNewPrefab(_starPrefab);
            Container.BindFactory<Team, Ship, Ship.Factory>().FromComponentInNewPrefab(_shipPrefab);
        }
    }
}