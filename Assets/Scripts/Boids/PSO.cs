using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PSO : MonoBehaviour
{
    public Transform target;           // Ссылка на цель
    public float maxSpeed = 5f;        // Максимальная скорость
    public float minDistanceToTarget = 2f; // Минимальное допустимое расстояние до цели

    public float inertiaWeight = 0.5f; // Вес инерции
    public float cognitiveWeight = 1.5f; // Вес личного опыта
    public float socialWeight = 2.0f; // Вес коллективного опыта
    public float neighborRadius = 3f;  // Радиус взаимодействия

    private Vector2 velocity;
    private Vector2 personalBest; // Лучшая личная позиция
    private static Vector2 globalBest; // Глобально лучшая позиция среди всех агентов

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        personalBest = rb.position; // Начальная лучшая позиция — стартовая
        if (globalBest == Vector2.zero)
            globalBest = personalBest; // Инициализация глобального лучшего значения
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;
        Vector2 targetPosition = target.position;

        // Обновление личной лучшей позиции
        if (Vector2.Distance(currentPosition, targetPosition) < Vector2.Distance(personalBest, targetPosition))
        {
            personalBest = currentPosition;
        }

        // Обновление глобальной лучшей позиции
        if (Vector2.Distance(personalBest, targetPosition) < Vector2.Distance(globalBest, targetPosition))
        {
            globalBest = personalBest;
        }

        // Рассчитываем компоненты скорости
        Vector2 inertia = inertiaWeight * velocity;
        Vector2 cognitive = cognitiveWeight * Random.Range(0f, 1f) * (personalBest - currentPosition);
        Vector2 social = socialWeight * Random.Range(0f, 1f) * (globalBest - currentPosition);

        // Итоговая скорость
        velocity = inertia + cognitive + social;

        // Проверяем минимальное расстояние до цели
        float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);
        if (distanceToTarget < minDistanceToTarget)
        {
            Vector2 avoidForce = -(targetPosition - currentPosition).normalized * (1 - distanceToTarget / minDistanceToTarget) * maxSpeed;
            velocity += avoidForce;
        }

        // Ограничиваем скорость
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

        // Перемещаем агента
        rb.velocity = velocity;

        // Плавный поворот к направлению движения
        if (velocity.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            rb.rotation = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * 5f);
        }
    }

    // Визуализация
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, neighborRadius);  // Радиус взаимодействия с соседями

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToTarget);  // Минимальное расстояние до цели
    }
}
