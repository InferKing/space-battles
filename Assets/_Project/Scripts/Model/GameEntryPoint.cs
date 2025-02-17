using _Project.Scripts.Model.Core;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Model
{   
    public class GameEntryPoint : MonoInstaller
    {
        [SerializeField] private Star _starPrefab;
        [SerializeField] private Ship _shipPrefab;
        
        public override void InstallBindings()
        {
            // TODO: откуда берется Instance FieldParameters?
            // Container.Bind<FieldParameters>().FromInstance()
            
            Container.BindFactory<Star, Star.Factory>().FromComponentInNewPrefab(_starPrefab);
            Container.BindFactory<Team, Ship, Ship.Factory>().FromComponentInNewPrefab(_shipPrefab);
        }
    }
}