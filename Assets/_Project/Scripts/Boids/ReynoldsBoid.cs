using UnityEngine;

namespace _Project.Scripts.Boids
{
    public class ReynoldsBoid : MonoBehaviour, IBoid
    {
        
        /// <summary>
        /// В основе модели лежит классическая модель поведения роевых агентов (Boids) Крэйга Рейнольдса, которая
        /// включает три ключевых правила:
        /// 1. Притяжение к центру (Cohesion).
        /// 2. Выравнивание скорости (Alignment).
        /// 3. Разделение (Separation).
        ///
        /// Дополнительно были реализованы: избегание препятствий (Avoidance), приятжение к цели (Target attraction).
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

        [field: SerializeField, Tooltip("Вес притяжения к цели")]
        public float TargetAttractionWeight { get; private set; } = 3f;

        [field: SerializeField, Header("Цель")]
        public Transform Target { get; private set; }

        public Vector2 Velocity { get; private set; }
        
        private Rigidbody2D _rb;
        private BoidManager _manager;

        private void Start()
        {
            Velocity = Random.insideUnitCircle * StartSpeed;
            _manager = FindFirstObjectByType<BoidManager>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
            RotateTowards();
        }

        private Vector2 Cohesion()
        {
            var center = Vector2.zero;
            var count = 0;
            
            foreach (var boid in _manager.Boids)
            {
                if ((ReynoldsBoid)boid == this) continue;

                if (!(Vector2.Distance(transform.position, boid.Position) < NeighborRadius)) continue;
                
                center += (Vector2)boid.Position;
                count++;
            }

            return count > 0 ? (center / count - (Vector2)transform.position).normalized : Vector2.zero;
        }

        private Vector2 Alignment()
        {
            var avgVelocity = Vector2.zero;
            var count = 0;
            
            foreach (var boid in _manager.Boids)
            {
                if ((ReynoldsBoid)boid == this) continue;

                if (!(Vector2.Distance(transform.position, boid.Position) < NeighborRadius)) continue;
                
                avgVelocity += boid.Velocity;
                count++;
            }

            return count > 0 ? (avgVelocity / count).normalized : Vector2.zero;
        }

        private Vector2 Separation()
        {
            var avoid = Vector2.zero;
            
            foreach (var boid in _manager.Boids)
            {
                if ((ReynoldsBoid)boid == this) continue;
                
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

        public Vector3 Position => transform.position;
        public void Move()
        {
            var cohesion = Cohesion() * CohesionWeight;
            var alignment = Alignment() * AlignmentWeight;
            var separation = Separation() * SeparationWeight;
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
            var velocity = Velocity;
            
            var smoothedAngle = Mathf.SmoothDampAngle(_rb.rotation, targetAngle, ref velocity.x,
                RotationSpeed * Time.fixedDeltaTime);
            _rb.rotation = smoothedAngle;
        }
    }
}