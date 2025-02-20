using UnityEngine;

namespace _Project.Scripts.Model
{
    public interface IMovable
    {
        Vector3 Position { get; }
        Vector2 Velocity { get; }
        void Move();
    }
}