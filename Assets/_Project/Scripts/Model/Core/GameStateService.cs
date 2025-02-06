using System;
using UniRx;
using Zenject;

namespace _Project.Scripts.Model.Core
{
    public class GameStateService : IInitializable, IDisposable
    {
        // Класс, отвечающий за изменение состояния модели. Только он меняет состояние. 
        
        private readonly IGameStateModel _gameStateModel;
        // TODO: что вместо IStarManager? Там должна быть информация по звездам, проверка состояний и т.п. 
        private readonly IStarManager _starManager;
        private readonly IPlayerProgress _playerProgress;
        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        public GameStateService(IGameStateModel gameStateModel, IStarManager starManager, IPlayerProgress playerProgress)
        {
            _gameStateModel = gameStateModel;
            _starManager = starManager;
            _playerProgress = playerProgress;
        }
        
        public void Initialize()
        {
            _gameStateModel.StateChanged.Value = _playerProgress.HasCompletedTutorial ? GameState.Play : GameState.Tutorial;

            // возможно надо убрать
            _gameStateModel.StateChanged.Subscribe(OnGameStateChanged).AddTo(_disposable);
        }

        private void OnGameStateChanged(GameState gameState)
        {
            // TODO: реагировать на изменение состояния если нужно
        }
        
        // Пример того что может вызываться здесь
        public void SetPause()
        {
            _gameStateModel.StateChanged.Value = _gameStateModel.StateChanged.Value switch
            {
                GameState.Play => GameState.Pause,
                GameState.Pause => GameState.Play,
                _ => _gameStateModel.StateChanged.Value
            };
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}