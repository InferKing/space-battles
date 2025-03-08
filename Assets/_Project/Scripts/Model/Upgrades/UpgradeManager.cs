using System.Collections.Generic;
using _Project.Scripts.Model.XPSystem;
using UniRx;

namespace _Project.Scripts.Model.Upgrades
{
    public class UpgradeManager
    {
        private XpLevelSystem _levelSystem;
        
        public ReactiveDictionary<UpgradeType, int> Levels { get; }

        public UpgradeManager(List<Upgrade> upgrades, XpLevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
            
            var dictionary = new Dictionary<UpgradeType, int>();
            
            upgrades.ForEach(upgrade =>
            {
                dictionary[upgrade.Type] = 0;
            });
            
            Levels = new ReactiveDictionary<UpgradeType, int>(dictionary);
            // Levels.ObserveReplace().Subscribe(change =>
            // {
            //     // действия
            // });
        }

        public void CalcLevel(UpgradeType upgradeType)
        {
            Levels[upgradeType] += 1;
            _levelSystem.FreePoints.Value -= 1;
        }
    }
}