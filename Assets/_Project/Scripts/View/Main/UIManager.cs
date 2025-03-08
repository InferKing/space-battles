using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Model.Upgrades;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Main
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SkillView _starHealth;
        [SerializeField] private SkillView _agentHealth;
        [SerializeField] private SkillView _damage;
        [SerializeField] private SkillView _attackSpeed;
        [SerializeField] private SkillView _moveSpeed;
        [SerializeField] private SkillView _limitAgent;
        
        private List<Upgrade> _upgrades;
        
        [Inject]
        private void Construct(List<Upgrade> upgrades)
        {
            _upgrades = upgrades;
        }

        private void Start()
        {
            _starHealth.Init(_upgrades.First(upgrade => upgrade.Type == UpgradeType.PlanetHealth));
            _agentHealth.Init(_upgrades.First(upgrade => upgrade.Type == UpgradeType.AgentHealth));
            _damage.Init(_upgrades.First(upgrade => upgrade.Type == UpgradeType.Damage));
            _attackSpeed.Init(_upgrades.First(upgrade => upgrade.Type == UpgradeType.AttackSpeed));
            _moveSpeed.Init(_upgrades.First(upgrade => upgrade.Type == UpgradeType.MoveSpeed));
            _limitAgent.Init(_upgrades.First(upgrade => upgrade.Type == UpgradeType.AgentSpawn));
        }
    }
}