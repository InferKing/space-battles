namespace _Project.Scripts.Model.Upgrades
{
    public class AgentSpawnUpgrade : Upgrade
    {
        private readonly int _increasedBy; 
        
        public AgentSpawnUpgrade(UpgradeType upgradeType, int increasedBy) : base(upgradeType)
        {
            _increasedBy = increasedBy;
        }

        public override void Apply(Player.Player player)
        {
            player.ShipLimit.Value += _increasedBy;
        }
    }
}