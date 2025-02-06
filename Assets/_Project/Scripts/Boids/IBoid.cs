using _Project.Scripts.Model;

namespace _Project.Scripts.Boids
{
    public interface IBoid : IMovable
    {
        void RotateTowards();
    }
}