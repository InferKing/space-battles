using System;
using _Project.Scripts.Controller;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Model.Core.TargetSelector
{
    public class PlayerSelector : ITargetSelector, IInitializable, IDisposable
    {
        public class Factory : PlaceholderFactory<PlayerSelector> {}
        
        private InputController _inputController;
        
        public PlayerSelector(InputController inputController)
        {
            _inputController = inputController;
        }

        public ReactiveProperty<Vector2> Target { get; } = new(Vector2.zero);
        
        public void Initialize()
        {   
            _inputController.PlayerTouched += OnPlayerTouched;
        }
        
        private void OnPlayerTouched(Vector2 target)
        {
            Target.Value = target;
        }

        public void Dispose()
        {
            Target?.Dispose();
            _inputController.PlayerTouched -= OnPlayerTouched;
        }
    }
}