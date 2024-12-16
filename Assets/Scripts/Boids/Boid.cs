using UnityEngine;
using Utils;

namespace Boids
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Boid : MonoBehaviour, IStrategy
    {
        public Transform target;             // Ссылка на цель
        public float maxSpeed = 5f;          // Максимальная скорость
        public float minDistanceToTarget = 2f; // Минимальное расстояние до цели
        public float neighborRadius = 3f;    // Радиус взаимодействия с соседями
        public float separationForce = 1.5f; // Сила избегания соседей
        public float targetForce = 1f;       // Сила притяжения к цели
        public float rotationSpeed = 5f;     // Скорость поворота

        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            Vector2 currentPosition = rb.position;
            Vector2 targetPosition = target.position;

            // Рассчитываем расстояние до цели
            float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

            // Рассчитываем силу притяжения или отталкивания от цели
            Vector2 forceToTarget = Vector2.zero;
            if (distanceToTarget < minDistanceToTarget)
            {
                float avoidStrength = 1 - (distanceToTarget / minDistanceToTarget); 
                forceToTarget = -(targetPosition - currentPosition).normalized * targetForce * avoidStrength * 3f;
            }
            else
            {
                forceToTarget = (targetPosition - currentPosition).normalized * targetForce;
            }

            // Рассчитываем силу избегания соседей
            Vector2 separation = Vector2.zero;
            Collider2D[] neighbors = Physics2D.OverlapCircleAll(currentPosition, neighborRadius, LayerMask.GetMask("Agent"));
            foreach (var neighbor in neighbors)
            {
                if (neighbor.gameObject != gameObject)
                {
                    Vector2 toNeighbor = (Vector2)neighbor.transform.position - currentPosition;
                    separation -= toNeighbor.normalized / Mathf.Max(toNeighbor.magnitude, 0.01f);
                }
            }

            // Итоговая сила
            Vector2 totalForce = forceToTarget + separation * separationForce;

            // Обновляем скорость через физику
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + totalForce * Time.fixedDeltaTime, maxSpeed);

            // Рассчитываем угол направления
            if (rb.velocity.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            
                // Плавный поворот с интерполяцией
                float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * rotationSpeed);
                rb.rotation = smoothAngle;
            }
        }

        // Визуализация в редакторе
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, neighborRadius);  // Радиус взаимодействия с соседями

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minDistanceToTarget);  // Минимальное расстояние до цели
        }

        public void Handle()
        {
        
        }
    }
}
