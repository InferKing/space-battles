using System.Collections.Generic;
using _Project.Scripts.Model.Core;
using UnityEngine;

namespace _Project.Scripts.Boids
{
    public class ReynoldsBoid : MonoBehaviour, IBoid
    {
        // TODO: Вынести 
        /// <summary>
        /// В основе модели лежит классическая модель поведения роевых агентов (Boids) Крэйга Рейнольдса, которая
        /// включает три ключевых правила:
        /// 1. Притяжение к центру (Cohesion).
        /// 2. Выравнивание скорости (Alignment).
        /// 3. Разделение (Separation).
        /// </summary>
        
        [field: SerializeField, Header("Основные параметры")]
        public float StartSpeed { get; private set; } = 5f;

        [field: SerializeField] public float MaxSpeed { get; private set; } = 7f;

        [field: SerializeField] public float Acceleration { get; private set; } = 10f;

        [field: SerializeField] public float NeighborRadius { get; private set; } = 3f;

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

        [field: SerializeField, Tooltip("Максимальный вес притяжения к цели")]
        public float MaxTargetAttractionWeight { get; private set; } = 3f;

        [field: SerializeField, Tooltip("Минимальный вес притяжения к цели")]
        public float MinTargetAttractionWeight { get; private set; } = 0.5f;

        [field: SerializeField, Header("Цель")]
        public Vector2 Target { get; set; }

        public Vector2 Velocity { get; private set; }
        public Vector3 Position => transform.position;
        
        private Rigidbody2D _rb;
        private List<Ship> _ships;
        private bool _isLockAutoRotation;
        
        private void Start()
        {
            Velocity = Random.insideUnitCircle * StartSpeed;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
            
            if (!_isLockAutoRotation)
            {
                Rotate(_rb.linearVelocity);
            }
        }

        public void Init(List<Ship> ships)
        {
            _ships = ships;
        }
        
        private float CalculateTargetAttractionWeight()
        {
            var distance = Vector2.Distance(transform.position, Target);
            return Mathf.Lerp(MinTargetAttractionWeight, MaxTargetAttractionWeight, distance / NeighborRadius);
        }

        private Vector2 Cohesion()
        {
            var center = Vector2.zero;
            var count = 0;
            
            foreach (var boid in _ships)
            {
                if (!(Vector2.Distance(transform.position, boid.Position) < NeighborRadius)) continue;
                
                center += (Vector2)boid.transform.position;
                count++;
            }

            return count > 0 ? (center / count - (Vector2)transform.position).normalized : Vector2.zero;
        }

        private Vector2 Alignment()
        {
            var avgVelocity = Vector2.zero;
            var count = 0;
            
            foreach (var boid in _ships)
            {
                if (!(Vector2.Distance(transform.position, boid.Position) < NeighborRadius)) continue;
                
                avgVelocity += boid.Velocity;
                count++;
            }

            return count > 0 ? (avgVelocity / count).normalized : Vector2.zero;
        }

        private Vector2 Separation()
        {
            var avoid = Vector2.zero;
            
            foreach (var boid in _ships)
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
            var hit = Physics2D.CircleCast(transform.position, AvoidanceRadius, Velocity.normalized,
                AvoidanceRadius);
            
            return hit.collider ? Vector2.Reflect(Velocity.normalized, hit.normal) : Vector2.zero;
        }

        public void Move()
        {
            var cohesion = Cohesion() * CohesionWeight;
            var alignment = Alignment() * AlignmentWeight;
            var separation = Separation() * SeparationWeight;
            var avoidance = AvoidObstacles() * AvoidanceWeight;
            var targetAttractionWeight = CalculateTargetAttractionWeight();
            var targetAttraction = (Target - (Vector2)transform.position).normalized * targetAttractionWeight;

            var acceleration = cohesion + alignment + separation + avoidance + targetAttraction;
            _rb.AddForce(acceleration * (Acceleration * Time.fixedDeltaTime));

            if (_rb.linearVelocity.magnitude > MaxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * MaxSpeed;
            }
        }

        public void Rotate(Vector2 direction)
        {
            var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            var velocity = Velocity;
            
            var smoothedAngle = Mathf.SmoothDampAngle(_rb.rotation, targetAngle, ref velocity.x,
                RotationSpeed * Time.fixedDeltaTime);
            _rb.rotation = smoothedAngle;
        }

        public void ToggleAutoRotation(bool isLock)
        {
            _isLockAutoRotation = isLock;
        }
    }
}