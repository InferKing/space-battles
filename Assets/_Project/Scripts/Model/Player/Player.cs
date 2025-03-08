using _Project.Scripts.Configs;
using _Project.Scripts.Model.Core.TargetSelector;
using _Project.Scripts.Model.Upgrades;
using _Project.Scripts.Model.XPSystem;
using UniRx;

namespace _Project.Scripts.Model.Player
{
    public class Player
    {
        private readonly PlayerData _playerData;
        
        public Player(PlayerData playerData, UpgradeManager upgradeManager, XpLevelSystem xpLevelSystem, 
            ITargetSelector targetSelector)
        {
            _playerData = playerData;
            LevelSystem = xpLevelSystem;
            UpgradeManager = upgradeManager;
            TargetSelector = targetSelector;

            MaxSpeed = new ReactiveProperty<float>(_playerData.BaseMaxSpeed);
            Acceleration = new ReactiveProperty<float>(_playerData.BaseAcceleration);
            AgentHealth = new ReactiveProperty<float>(_playerData.BaseShipHealth);
            StarHealth = new ReactiveProperty<float>(_playerData.BaseStarHealth);
            Damage = new ReactiveProperty<float>(_playerData.BaseDamage);
            AttackSpeed = new ReactiveProperty<float>(_playerData.BaseAttackSpeed);
            ProjectileSpeed = new ReactiveProperty<float>(_playerData.BaseProjectileSpeed);
            ShipLimit = new ReactiveProperty<int>(_playerData.BaseShipLimit);
            
            CurrentShipLimit = 0;
        }
        
        public XpLevelSystem LevelSystem { get; }
        public UpgradeManager UpgradeManager { get; }
        public ITargetSelector TargetSelector { get; }
        public ReactiveProperty<float> MaxSpeed { get; }
        public ReactiveProperty<float> Acceleration { get; }
        public ReactiveProperty<float> AgentHealth { get; }
        public ReactiveProperty<float> StarHealth { get; }
        public ReactiveProperty<float> Damage { get; }
        public ReactiveProperty<float> AttackSpeed { get; }
        public ReactiveProperty<float> ProjectileSpeed { get; }
        public ReactiveProperty<int> ShipLimit { get; }
        public int CurrentShipLimit { get; set; }
        public Team Team => _playerData.Team;

        public void ApplyUpgrade(Upgrade upgrade)
        {
            upgrade.Apply(this);
            UpgradeManager.CalcLevel(upgrade.Type);
        }
    }
}