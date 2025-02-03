using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Boids
{
    public class BoidManager : MonoBehaviour
    {
        public List<IBoid> Boids { get; private set; } = new();

        private void Start()
        {
            FindAllBoids();
        }

        private void FindAllBoids()
        {
            var allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
            Boids = allObjects.OfType<IBoid>().ToList();
        }
    }
}
