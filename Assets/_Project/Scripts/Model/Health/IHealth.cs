namespace _Project.Scripts.Model.Health
{
    public interface IHealth
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        bool IsAlive { get; }
        Team Team { get; }
        
        void TakeDamage(Team attackerTeam, float amount);
    }
}