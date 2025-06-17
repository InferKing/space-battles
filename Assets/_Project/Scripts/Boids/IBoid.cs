using System.Collections.Generic;
using _Project.Scripts.Model;
using _Project.Scripts.Model.Core;
using UnityEngine;

namespace _Project.Scripts.Boids
{
    public interface IBoid : IMovable
    {
        public Vector2 Target { get; set; }
        public float Acceleration { get; set; }
        public float MaxSpeed { get; set; }
        void Rotate(Vector2 direction);
        void ToggleAutoRotation(bool isLock);
        void Init(List<Ship> teammates);
    }
}