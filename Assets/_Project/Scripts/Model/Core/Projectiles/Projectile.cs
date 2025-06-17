using System.Collections;
using _Project.Scripts.Model.Health;
using UnityEngine;

namespace _Project.Scripts.Model.Core.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private ParticleSystem _explosionEffect;
    
        private const float ExplosionRadius = 0.4f;
        
        private Team _team;
        private float _damage;
        private float _speed;
        private Vector3 _targetPosition;

        public void Init(Team team, float damage, float speed, Vector3 position)
        {
            _team = team;
            _damage = damage;
            _speed = speed;
            _targetPosition = position;

            StartCoroutine(MoveToTarget());
        }

        private IEnumerator MoveToTarget()
        {
            while (Vector3.Distance(transform.position, _targetPosition) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
                yield return null;
            }

            Explode();
        }

        private void Explode()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<IHealth>(out var healthUnit) && healthUnit.Team != _team)
                {
                    healthUnit.TakeDamage(_team, _damage);
                }
            }

            if (_explosionEffect)
            {
                var explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                explosion.Play();
                Destroy(explosion.gameObject, explosion.main.duration);
            }

            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
        }
    }

}