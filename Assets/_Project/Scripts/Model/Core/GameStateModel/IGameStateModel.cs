using UniRx;

namespace _Project.Scripts.Model.Core
{
    public interface IGameStateModel
    {
        ReactiveProperty<GameState> StateChanged { get; }
    }
}