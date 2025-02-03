using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Boids
{
    public class LimitedInfluenceBoid : MonoBehaviour, IBoid
    {
        [field: SerializeField, Header("Основные параметры")]
        public float StartSpeed { get; private set; } = 5f;

        [field: SerializeField] public float MaxSpeed { get; private set; } = 7f;

        [field: SerializeField] public float Acceleration { get; private set; } = 10f;

        [field: SerializeField] public float NeighborRadius { get; private set; } = 3f;

        [field: SerializeField] public int MaxNeighbors { get; private set; } = 5;

        [field: SerializeField] public float SeparationRadius { get; private set; } = 1f;

        [field: SerializeField] public float AvoidanceRadius { get; private set; } = 2f;

        [field: SerializeField, Header("Вес параметров поведения"), Tooltip("Вес притяжения к центру группы")]
        public float CohesionWeight { get; private set; } = 1f;

        [field: SerializeField, Tooltip("Вес выравнивания скорости с соседями")]
        public float AlignmentWeight { get; private set; } = 1f;

        [field: SerializeField, Tooltip("Вес разделения агентов для избегания столкновений")]
        public float SeparationWeight { get; private set; } = 1.5f;

        [field: SerializeField, Tooltip("Вес избегания препятствий")]
        public float AvoidanceWeight { get; private set; } = 2f;

        [field: SerializeField, Tooltip("Скорость вращения агента")]
        public float RotationSpeed { get; private set; } = 2f;

        [field: SerializeField, Tooltip("Вес притяжения к цели")]
        public float TargetAttractionWeight { get; private set; } = 3f;

        [field: SerializeField, Header("Цели")]
        public Transform Target { get; private set; }

        private Vector2 _velocity;
        private Rigidbody2D _rb;
        private BoidManager _manager;

        private void Start()
        {
            _velocity = Random.insideUnitCircle * StartSpeed;
            _manager = FindFirstObjectByType<BoidManager>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
            RotateTowards();
        }

        public Vector3 Position => transform.position;
        public Vector2 Velocity => _rb.linearVelocity;

        public void Move()
        {
            var neighbors = GetLimitedNeighbors();
            
            var cohesion = Cohesion(neighbors) * CohesionWeight;
            var alignment = Alignment(neighbors) * AlignmentWeight;
            var separation = Separation(neighbors) * SeparationWeight;
            var avoidance = AvoidObstacles() * AvoidanceWeight;
            var targetAttraction =
                (Target
                    ? (Vector2)(Target.position - transform.position).normalized * TargetAttractionWeight
                    : Vector2.zero);

            var acceleration = cohesion + alignment + separation + avoidance + targetAttraction;
            _rb.AddForce(acceleration * (Acceleration * Time.fixedDeltaTime));

            if (_rb.linearVelocity.magnitude > MaxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * MaxSpeed;
            }
        }

        public void RotateTowards()
        {
            var targetAngle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg - 90f;
            var smoothedAngle = Mathf.SmoothDampAngle(_rb.rotation, targetAngle, ref _velocity.x,
                RotationSpeed * Time.fixedDeltaTime);
            _rb.rotation = smoothedAngle;
        }

        private Vector2 Cohesion(List<IBoid> neighbors)
        {
            if (neighbors.Count == 0) return Vector2.zero;

            var center = neighbors.Aggregate(Vector2.zero, (current, boid) => current + (Vector2)boid.Position);

            return (center / neighbors.Count - (Vector2)transform.position).normalized;
        }

        private Vector2 Alignment(List<IBoid> neighbors)
        {
            if (neighbors.Count == 0) return Vector2.zero;

            var avgVelocity = neighbors.Aggregate(Vector2.zero, (current, boid) => current + boid.Velocity);

            return (avgVelocity / neighbors.Count).normalized;
        }

        private Vector2 Separation(List<IBoid> neighbors)
        {
            var avoid = Vector2.zero;
            
            foreach (var boid in neighbors)
            {
                var distance = Vector2.Distance(transform.position, boid.Position);
                
                if (distance < SeparationRadius && distance > 0)
                {
                    avoid += ((Vector2)transform.position - (Vector2)boid.Position) / distance;
                }
            }

            return avoid.normalized;
        }

        private Vector2 AvoidObstacles()
        {
            var hit = Physics2D.CircleCast(transform.position, AvoidanceRadius, _velocity.normalized,
                AvoidanceRadius);
            
            return hit.collider ? Vector2.Reflect(_velocity.normalized, hit.normal) : Vector2.zero;
        }

        private List<IBoid> GetLimitedNeighbors()
        {
            var neighbors = new List<IBoid>();
            
            foreach (var boid in _manager.Boids)
            {
                if ((LimitedInfluenceBoid)boid == this) continue;
                
                if (Vector2.Distance(transform.position, boid.Position) < NeighborRadius)
                {
                    neighbors.Add(boid);
                }
            }

            neighbors.Sort((a, b) => Vector2.Distance(transform.position, a.Position)
                .CompareTo(Vector2.Distance(transform.position, b.Position)));

            return neighbors.Count > MaxNeighbors ? neighbors.GetRange(0, MaxNeighbors) : neighbors;
        }
    }
}