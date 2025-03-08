using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Boids;
using _Project.Scripts.Model.Health;
using _Project.Scripts.View.Main;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Model.Core
{
    public class Ship : MonoBehaviour, IHealth, IAttacker
    {
        [SerializeField] private ShipView _view;
        [SerializeField] private LayerMask _targetLayer;

        private IBoid _boid;
        private BoidManager _boidManager;
        private Player.Player _player;
        private IHealth _enemy;
        
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
        public bool IsAlive { get; }
        public float AttackRange { get; }
        public float AttackCooldown { get; }
        public float Damage { get; }
        public Vector2 Velocity => _boid.Velocity;
        public Vector3 Position => _boid.Position;
        public Team Team { get; private set; }
        
        [Inject]
        private void Construct(Team team, Player.Player player, BoidManager boidManager)
        {
            Team = team;
            _boidManager = boidManager;
            _player = player;
        }

        private void Start()
        {
            _boid = GetComponent<IBoid>();
            _boid.Init(_boidManager.GetTeammates(Team));
            
            _player.TargetSelector.Target.Subscribe(newTarget =>
            {
                _boid.Target = newTarget;
            }).AddTo(this);
            _boid.Target = transform.position;
            
            _view.UpdateView(Team);
        }

        private void Update()
        {
            var target = FindTarget();
            // дальнейшая логика по атаке с кулдауном
        }

        private IHealth FindTarget()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, AttackRange, _targetLayer);
            IHealth target = null;
            float targetDistance = float.MaxValue;
            
            foreach (var objCollider in colliders)
            {
                IHealth healthUnit;
                if (objCollider && objCollider.TryGetComponent(out healthUnit))
                {
                    if (healthUnit.Team == Team) continue;

                    var possibleDistance = Vector2.Distance(transform.position, objCollider.transform.position);
                    
                    if (target != null && targetDistance > possibleDistance)
                    {
                        targetDistance = possibleDistance;
                        target = healthUnit;
                    }
                    else if (target == null)
                    {
                        target = healthUnit;
                        targetDistance = possibleDistance;
                    }
                }
            }

            return target;
        }

        public void TakeDamage(Team attackerTeam, float amount)
        {
            
        }
        
        public void Attack(IHealth target)
        {
            
        }
        
        public class Factory : PlaceholderFactory<Team, Player.Player, Ship> {}
        
    }
}