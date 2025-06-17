using UniRx;
using UnityEngine;

namespace _Project.Scripts.Model.Health
{
    public interface IHealth
    {
        Vector3 Position { get; }
        ReactiveProperty<float> CurrentHealth { get; }
        float MaxHealth { get; }
        ReactiveProperty<bool> IsAlive { get; }
        Team Team { get; }
        
        void TakeDamage(Team attackerTeam, float amount);
    }
}