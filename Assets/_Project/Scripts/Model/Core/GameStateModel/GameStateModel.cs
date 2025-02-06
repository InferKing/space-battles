using UniRx;

namespace _Project.Scripts.Model.Core
{
    public class GameStateModel : IGameStateModel
    {
        public ReactiveProperty<GameState> StateChanged { get; }

        public GameStateModel(GameState gameState)
        {
            StateChanged = new ReactiveProperty<GameState>(gameState);
        }
    }
}