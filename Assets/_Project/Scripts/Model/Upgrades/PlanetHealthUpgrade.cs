namespace _Project.Scripts.Model.Upgrades
{
    public class PlanetHealthUpgrade : Upgrade
    {
        private readonly float _increasedBy;
        
        public PlanetHealthUpgrade(UpgradeType upgradeType, float increasedBy) : base(upgradeType)
        {
            _increasedBy = increasedBy;
        }

        public override void Apply(Player.Player player)
        {
            player.StarHealth.Value += _increasedBy;
        }
    }
}