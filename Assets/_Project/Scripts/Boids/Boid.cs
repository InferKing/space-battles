using UnityEngine;

namespace _Project.Scripts.Boids
{
    public class Boid : MonoBehaviour
    {
        [field: SerializeField, Header("Основные параметры")]
        public float Speed { get; private set; } = 5f;

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

        private Vector2 _velocity;
        private Rigidbody2D _rb;
        private BoidManager _manager;

        private void Start()
        {
            _velocity = Random.insideUnitCircle * Speed;
            _manager = FindObjectOfType<BoidManager>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 cohesion = Cohesion() * CohesionWeight;
            Vector2 alignment = Alignment() * AlignmentWeight;
            Vector2 separation = Separation() * SeparationWeight;
            Vector2 avoidance = AvoidObstacles() * AvoidanceWeight;
            Vector2 targetAttraction =
                (Target
                    ? (Vector2)(Target.position - transform.position).normalized * TargetAttractionWeight
                    : Vector2.zero);

            Vector2 acceleration = cohesion + alignment + separation + avoidance + targetAttraction;
            _rb.AddForce(acceleration * (Acceleration * Time.fixedDeltaTime));

            if (_rb.linearVelocity.magnitude > MaxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * MaxSpeed;
            }

            float targetAngle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg - 90f;
            float smoothedAngle = Mathf.SmoothDampAngle(_rb.rotation, targetAngle, ref _velocity.x,
                RotationSpeed * Time.fixedDeltaTime);
            _rb.rotation = smoothedAngle;
        }

        private Vector2 Cohesion()
        {
            Vector2 center = Vector2.zero;
            int count = 0;
            foreach (var boid in _manager.Boids)
            {
                if (boid == this) continue;
                if (Vector2.Distance(transform.position, boid.transform.position) < NeighborRadius)
                {
                    center += (Vector2)boid.transform.position;
                    count++;
                }
            }

            return count > 0 ? (center / count - (Vector2)transform.position).normalized : Vector2.zero;
        }

        private Vector2 Alignment()
        {
            Vector2 avgVelocity = Vector2.zero;
            int count = 0;
            foreach (var boid in _manager.Boids)
            {
                if (boid == this) continue;
                if (Vector2.Distance(transform.position, boid.transform.position) < NeighborRadius)
                {
                    avgVelocity += boid._velocity;
                    count++;
                }
            }

            return count > 0 ? (avgVelocity / count).normalized : Vector2.zero;
        }

        private Vector2 Separation()
        {
            Vector2 avoid = Vector2.zero;
            foreach (var boid in _manager.Boids)
            {
                if (boid == this) continue;
                float distance = Vector2.Distance(transform.position, boid.transform.position);
                if (distance < SeparationRadius && distance > 0)
                {
                    avoid += ((Vector2)transform.position - (Vector2)boid.transform.position) / distance;
                }
            }

            return avoid.normalized;
        }

        private Vector2 AvoidObstacles()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, AvoidanceRadius, _velocity.normalized,
                AvoidanceRadius);
            if (hit.collider != null)
            {
                return Vector2.Reflect(_velocity.normalized, hit.normal);
            }

            return Vector2.zero;
        }
    }
}