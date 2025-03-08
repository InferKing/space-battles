namespace _Project.Scripts.Model.Upgrades
{
    public class DamageUpgrade : Upgrade
    {
        private readonly float _increasedBy; 
        
        public DamageUpgrade(UpgradeType upgradeType, float increasedBy) : base(upgradeType)
        {
            _increasedBy = increasedBy;
        }

        public override void Apply(Player.Player player)
        {
            player.Damage.Value += _increasedBy;
        }
    }
}