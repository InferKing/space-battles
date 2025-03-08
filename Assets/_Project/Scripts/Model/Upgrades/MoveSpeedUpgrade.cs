namespace _Project.Scripts.Model.Upgrades
{
    public class MoveSpeedUpgrade : Upgrade
    {
        private readonly float _increasedBySpeed;
        private readonly float _increasedByAcceleration;
        
        public MoveSpeedUpgrade(UpgradeType upgradeType, float increasedBySpeed, float increasedByAcceleration) : base(upgradeType)
        {
            _increasedBySpeed = increasedBySpeed;
            _increasedByAcceleration = increasedByAcceleration;
        }

        public override void Apply(Player.Player player)
        {
            player.MaxSpeed.Value += _increasedBySpeed;
            player.Acceleration.Value += _increasedByAcceleration;
        }
    }
}