using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FishSchoolSearch : MonoBehaviour
{
    public Transform target;             // Цель
    public float maxSpeed = 5f;          // Максимальная скорость
    public float attractionWeight = 1.5f; // Сила притяжения к цели
    public float schoolWeight = 1.0f;    // Влияние стаи
    public float repulsionWeight = 2.0f; // Сила отталкивания
    public float neighborRadius = 3f;    // Радиус соседей

    private Vector2 velocity;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Random.insideUnitCircle * maxSpeed;
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;

        // Сила притяжения к цели
        Vector2 attractionForce = (target.position - transform.position).normalized * attractionWeight;

        // Сила избегания соседей
        Vector2 repulsionForce = Vector2.zero;
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, neighborRadius, LayerMask.GetMask("Agent"));

        foreach (var neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject)
            {
                Vector2 toNeighbor = (Vector2)neighbor.transform.position - currentPosition;
                repulsionForce -= toNeighbor.normalized * repulsionWeight / toNeighbor.magnitude;
            }
        }

        // Обновление скорости
        velocity += attractionForce + repulsionForce;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

        // Перемещение
        rb.velocity = velocity;

        // Плавный поворот
        if (velocity.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            rb.rotation = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * 5f);
        }
    }

    // Визуализация
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, neighborRadius);  // Радиус взаимодействия
    }
}
