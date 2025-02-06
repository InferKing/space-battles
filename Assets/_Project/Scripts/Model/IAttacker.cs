using _Project.Scripts.Model.Health;

namespace _Project.Scripts.Model
{
    public interface IAttacker
    {
        float AttackRange { get; }
        float AttackCooldown { get; }
        float Damage { get; }
    
        void Attack(IHealth target);
    }
}