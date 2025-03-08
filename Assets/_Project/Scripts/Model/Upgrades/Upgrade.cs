namespace _Project.Scripts.Model.Upgrades
{
    public abstract class Upgrade
    {
        public UpgradeType Type { get; }

        protected Upgrade(UpgradeType upgradeType)
        {
            Type = upgradeType;
        }

        public abstract void Apply(Player.Player player);
    }
}