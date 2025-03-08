using UniRx;
using UnityEngine;

namespace _Project.Scripts.Model.Core.TargetSelector
{
    public interface ITargetSelector
    {
        ReactiveProperty<Vector2> Target { get; }
    }
}