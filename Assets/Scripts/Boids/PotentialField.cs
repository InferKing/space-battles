using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PotentialField : MonoBehaviour
{
    public Transform target;               // Цель
    public float maxSpeed = 5f;            // Максимальная скорость

    public float attractionStrength = 10f; // Сила притяжения к цели
    public float repulsionStrength = 15f;  // Сила отталкивания
    public float avoidanceRadius = 2f;     // Радиус избегания

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;

        // Рассчитываем силу притяжения к цели
        Vector2 attractionForce = CalculateAttractionForce(currentPosition, target.position);

        // Рассчитываем силу отталкивания от других агентов
        Vector2 repulsionForce = CalculateRepulsionForce(currentPosition);

        // Итоговая сила
        Vector2 totalForce = attractionForce + repulsionForce;

        // Ограничиваем скорость
        rb.velocity = Vector2.ClampMagnitude(totalForce, maxSpeed);

        // Плавный поворот к направлению движения
        if (rb.velocity.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            rb.rotation = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * 5f);
        }
    }

    /// <summary>
    /// Рассчитывает силу притяжения к цели
    /// </summary>
    Vector2 CalculateAttractionForce(Vector2 currentPosition, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - currentPosition;
        float distance = direction.magnitude;

        // Притяжение уменьшается с расстоянием
        return direction.normalized * Mathf.Clamp(attractionStrength / distance, 0, maxSpeed);
    }

    /// <summary>
    /// Рассчитывает силу отталкивания от других агентов
    /// </summary>
    Vector2 CalculateRepulsionForce(Vector2 currentPosition)
    {
        Vector2 repulsionForce = Vector2.zero;
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(currentPosition, avoidanceRadius, LayerMask.GetMask("Agent"));

        foreach (var neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject)
            {
                Vector2 toNeighbor = (Vector2)neighbor.transform.position - currentPosition;
                float distance = toNeighbor.magnitude;

                if (distance > 0)
                {
                    // Отталкиваемся, если в зоне избегания
                    Vector2 repulsion = -toNeighbor.normalized * repulsionStrength / distance;
                    repulsionForce += repulsion;
                }
            }
        }

        return repulsionForce;
    }

    // Визуализация в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);  // Радиус избегания
    }
}
