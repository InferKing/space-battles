namespace _Project.Scripts.Model.Health
{
    public interface IHealable : IHealth
    {
        void Heal(float amount);
    }
}

 