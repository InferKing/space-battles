using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BatAlgorithm : MonoBehaviour
{
    public Transform target;          // Цель
    public float maxSpeed = 5f;       // Максимальная скорость
    public float minDistance = 1f;   // Минимальное расстояние до цели

    public float frequency = 0.5f;    // Начальная частота звука
    public float amplitude = 2f;      // Начальная амплитуда
    public float damping = 0.95f;     // Коэффициент затухания амплитуды

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;

        if (distance < minDistance)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Обновление параметров
        frequency += Random.Range(-0.1f, 0.1f);  // Случайная корректировка
        frequency = Mathf.Max(0.1f, frequency);
        amplitude *= damping;                   // Затухание амплитуды

        // Расчет нового направления
        Vector2 randomDirection = new Vector2(
            direction.x + Random.Range(-amplitude, amplitude),
            direction.y + Random.Range(-amplitude, amplitude)
        ).normalized;

        rb.velocity = randomDirection * Mathf.Clamp(distance * frequency, 0, maxSpeed);

        // Плавный поворот
        float targetAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        rb.rotation = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * 5f);
    }
}