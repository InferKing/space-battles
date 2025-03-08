using UniRx;
using UnityEngine;

namespace _Project.Scripts.Model.Core.TargetSelector
{
    public class BotSelector : ITargetSelector
    {
        // TODO: доделать логику для бота
        public ReactiveProperty<Vector2> Target { get; } = new(Vector2.zero);
    }
}