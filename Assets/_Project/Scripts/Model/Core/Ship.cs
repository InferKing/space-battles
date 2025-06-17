using System;
using System.Collections;
using _Project.Scripts.Boids;
using _Project.Scripts.Model.Core.Projectiles;
using _Project.Scripts.Model.Health;
using _Project.Scripts.View.Main;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Model.Core
{
    public class Ship : MonoBehaviour, IHealth, IAttacker
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private ShipView _view;
        [SerializeField] private LayerMask _targetLayer;

        private IBoid _boid;
        private BoidManager _boidManager;
        private Player.Player _player;
        private IHealth _enemy;
        private Coroutine _attackLogic;

        public ReactiveProperty<float> CurrentHealth { get; private set; } = new();
        public float MaxHealth { get; private set; }
        public ReactiveProperty<bool> IsAlive { get; } = new(true);
        public float AttackRange { get; private set; }
        public float AttackSpeed { get; private set; }
        public float AttackCooldown => 1 / AttackSpeed;
        public float Damage { get; private set; }
        public float MaxSpeed { get; private set; }
        public float Acceleration { get; private set; }
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

            InitializeStats();

            CurrentHealth.Value = MaxHealth;
            
            StartCoroutine(UpdateTargetInf());
        }

        private void Update()
        {
            _boid.ToggleAutoRotation(_enemy != null);
            
            if (_enemy != null)
            {
                try
                {
                    _boid.Rotate(_enemy.Position - transform.position);
                }
                catch (MissingReferenceException) { }
            }
        }

        private void InitializeStats()
        {
            _player.MaxSpeed.Subscribe(newValue =>
            {
                MaxSpeed = newValue;
                _boid.MaxSpeed = MaxSpeed;
            }).AddTo(this);

            _player.Acceleration.Subscribe(newValue =>
            {
                Acceleration = newValue;
                _boid.Acceleration = Acceleration;
            }).AddTo(this);

            _player.AgentHealth.Subscribe(newValue =>
            {
                MaxHealth = newValue;
            }).AddTo(this);

            _player.Damage.Subscribe(newValue =>
            {
                Damage = newValue;
            }).AddTo(this);

            _player.AttackSpeed.Subscribe(newValue =>
            {
                AttackSpeed = newValue;
            }).AddTo(this);

            _player.AttackRange.Subscribe(newValue =>
            {
                AttackRange = newValue;
            }).AddTo(this);
        }

        private IEnumerator UpdateTargetInf()
        {
            var wait = new WaitForSeconds(0.1f);
            
            while (IsAlive.Value)
            {
                var possibleEnemy = FindTarget();
                
                if (possibleEnemy != null && possibleEnemy != _enemy)
                {
                    _enemy = possibleEnemy;
                    
                    _attackLogic = StartCoroutine(AttackLogic());
                }

                float distance = 10000000f;

                try
                {
                    if (_enemy != null)
                        distance = Vector3.Distance(_enemy.Position, transform.position);
                }
                catch (MissingReferenceException)
                {
                    distance = 10000000f;
                }
                
                if (_enemy == null || distance > AttackRange)
                {
                    _enemy = null;
                    
                    if (_attackLogic != null)
                    {
                        StopCoroutine(_attackLogic);
                    }
                    _attackLogic = null;
                }

                yield return wait;
            }
        }

        private IEnumerator AttackLogic()
        {
            var waitCooldown = new WaitForSeconds(AttackCooldown);

            while (_enemy != null && _enemy.IsAlive.Value)
            {
                Attack(_enemy);
                yield return waitCooldown;
            }
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
            if (!IsAlive.Value) return;

            CurrentHealth.Value -= amount;
    
            if (CurrentHealth.Value <= 0)
            {
                CurrentHealth.Value = 0;

                IsAlive.Value = false;
            }
        }
        
        public void Attack(IHealth target)
        {
            if (target == null || !target.IsAlive.Value) return;

            var projectile = Instantiate(_projectile);
            projectile.transform.position = transform.position;
            projectile.Init(Team, Damage, _player.ProjectileSpeed.Value, target.Position);
        }
        
        public class Factory : PlaceholderFactory<Team, Player.Player, Ship> {}
    }
}