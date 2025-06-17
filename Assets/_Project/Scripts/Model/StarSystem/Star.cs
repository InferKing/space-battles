using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Model.Core;
using _Project.Scripts.Model.Health;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Model.StarSystem
{
    public class Star : MonoBehaviour, IHealable
    {
        [SerializeField] private StarView _view;

        public event Action<Ship> ShipSpawned;
        
        private List<StarConnection> _starConnections = new();
        private List<Player.Player> _players;
        private Ship.Factory _shipFactory;
        private Player.Player _player;
        private Coroutine _spawner;
        
        [Inject]
        private void Construct(Ship.Factory shipFactory, List<Player.Player> players)
        {
            _shipFactory = shipFactory;
            _players = players;
        }
        
        private void Start()
        {
            _view.UpdateView(Team);
        }

        public void Init(Team team)
        {
            Team = team;
            _player = _players.First(player => player.Team == Team);

            CurrentHealth = new ReactiveProperty<float>(_player.StarHealth.Value);
            MaxHealth = CurrentHealth.Value;
            _view.UpdateHealth(CurrentHealth.Value);
            
            _player.StarHealth.Skip(1).Subscribe(change =>
            {
                CurrentHealth.Value += change - MaxHealth;
                MaxHealth = change;
                _view.UpdateHealth(CurrentHealth.Value);
            }).AddTo(this);

            _spawner = StartCoroutine(SpawnAgent());
        }

        public Vector3 Position => transform.position;
        public ReactiveProperty<float> CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public ReactiveProperty<bool> IsAlive { get; } = new();
        public Team Team { get; private set; }
        public List<Star> ConnectedStars { get; private set; } = new();
        public IReadOnlyList<StarConnection> Connections => _starConnections; 
        
        public void TakeDamage(Team attackerTeam, float amount)
        {
            CurrentHealth.Value = Mathf.Clamp(CurrentHealth.Value - amount, 0, MaxHealth);
            
            if (CurrentHealth.Value < 0.1f)
            {
                // TODO: делать запрос к некоторой системе для получения статов
                // TODO: нужно ли сообщать о смене команды?
                MaxHealth = 1000;
                CurrentHealth.Value = MaxHealth / 2;
                Team = attackerTeam;
                StopCoroutine(_spawner);
                _spawner = StartCoroutine(SpawnAgent());
                
                _view.UpdateView(Team);
            }
            
            _view.UpdateHealth(CurrentHealth.Value);
        }

        public void Heal(float amount)
        {
            CurrentHealth.Value = Mathf.Clamp(CurrentHealth.Value + amount, 0, MaxHealth);
            _view.UpdateHealth(CurrentHealth.Value);
        }

        public void AddConnection(StarConnection connection)
        {
            _starConnections.Add(connection);
        }

        private IEnumerator SpawnAgent()
        {
            var delay = new WaitForSeconds(Constants.SpawnDelay);
            
            while (CurrentHealth.Value > 0.1f)
            {
                yield return delay;
                
                if (_player.CurrentShipLimit <= _player.ShipLimit.Value)
                {
                    _player.CurrentShipLimit += 1;
                    var ship = SpawnInTorusArea();

                    ship.IsAlive.Subscribe(newValue =>
                    {
                        if (newValue == false)
                        {
                            _player.CurrentShipLimit--;
                        }
                    }).AddTo(this);
                    
                    ShipSpawned?.Invoke(ship);
                }
            }
        }

        private Ship SpawnInTorusArea()
        {
            var ship = _shipFactory.Create(Team, _player);
            
            float randomAngle = Random.Range(0f, Mathf.PI * 2);
            float randomRadius = Random.Range(1f, 2f);
            
            Vector3 spawnPosition = new Vector3(
                Mathf.Cos(randomAngle) * randomRadius + transform.position.x,
                Mathf.Sin(randomAngle) * randomRadius + transform.position.y,
                0f
            );

            float angle = Mathf.Atan2(spawnPosition.y, spawnPosition.x) * Mathf.Rad2Deg;

            ship.transform.position = spawnPosition;
            ship.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
            
            return ship;
        }


        public class Factory : PlaceholderFactory<Star> {}
    }
}