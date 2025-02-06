namespace _Project.Scripts.Model.Health
{
    public interface IHealth
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        bool IsAlive => CurrentHealth > Constants.Epsilon;
        Team Team { get; }
        
        void TakeDamage(float amount);
        void Die();
    }
}