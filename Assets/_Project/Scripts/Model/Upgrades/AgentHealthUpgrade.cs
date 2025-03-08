namespace _Project.Scripts.Model.Upgrades
{
    public class AgentHealthUpgrade : Upgrade
    {
        private readonly float _increasedBy;
        
        public AgentHealthUpgrade(UpgradeType upgradeType, float increasedBy) : base(upgradeType)
        {
            _increasedBy = increasedBy;
        }

        public override void Apply(Player.Player player)
        {
            player.AgentHealth.Value += _increasedBy;
        }
    }
}