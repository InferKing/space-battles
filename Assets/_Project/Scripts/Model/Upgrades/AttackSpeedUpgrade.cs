namespace _Project.Scripts.Model.Upgrades
{
    public class AttackSpeedUpgrade : Upgrade
    {
        private readonly float _increasedByAs;
        private readonly float _increasedByPS;
        
        public AttackSpeedUpgrade(UpgradeType upgradeType, float increasedByAs, float increasedByPS) : base(upgradeType)
        {
            _increasedByAs = increasedByAs;
            _increasedByPS = increasedByPS;
        }

        public override void Apply(Player.Player player)
        {
            player.AttackSpeed.Value += _increasedByAs;
            player.ProjectileSpeed.Value += _increasedByPS;
        }
    }
}