using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Boids
{
    public class BoidManager : MonoBehaviour
    {
        [field: SerializeField] public List<Boid> Boids { get; private set; } = new();
    }
}
