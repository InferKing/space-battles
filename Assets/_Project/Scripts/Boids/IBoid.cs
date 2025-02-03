using UnityEngine;

namespace _Project.Scripts.Boids
{
    public interface IBoid
    {
        Vector3 Position { get; }
        Vector2 Velocity { get; }
        void Move();
        void RotateTowards();
    }
}