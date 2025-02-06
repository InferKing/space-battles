using _Project.Scripts.Model;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PLayer data", menuName = "Config/Player data")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public Team Team { get; private set; }
        [field: SerializeField] public float BaseMaxSpeed { get; private set; }
        [field: SerializeField] public float BaseAcceleration { get; private set; }
        [field: SerializeField] public float BaseShipHealth { get; private set; }
        [field: SerializeField] public float BaseStarHealth { get; private set; }
        [field: SerializeField] public float BaseDamage { get; private set; }
        [field: SerializeField] public float BaseAttackSpeed { get; private set; }
        [field: SerializeField] public float BaseProjectileSpeed { get; private set; }
        [field: SerializeField] public float BaseShipLimit { get; private set; }
    }
}