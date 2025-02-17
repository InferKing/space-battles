using _Project.Scripts.Model;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PLayer data", menuName = "Config/Player data")]
    public class PlayerData : ScriptableObject
    {
        // TODO: реализовать кастомный атрибут Max и в него пихнуть значения из Constants, добавить к каждому полю с атрибутом Min
        [field: SerializeField] public Team Team { get; private set; }
        [field: SerializeField, Min(Constants.MinBaseSpeed)] public float BaseMaxSpeed { get; private set; } = 3f;
        [field: SerializeField, Min(Constants.MinBaseAcceleration)] public float BaseAcceleration { get; private set; } = 5f;
        [field: SerializeField, Min(Constants.MinBaseShipHealth)] public float BaseShipHealth { get; private set; } = 5f;
        [field: SerializeField, Min(Constants.MinBaseStarHealth)] public float BaseStarHealth { get; private set; } = 500f;
        [field: SerializeField, Min(Constants.MinBaseDamage)] public float BaseDamage { get; private set; } = 3f;
        [field: SerializeField, Min(Constants.MinBaseAttackSpeed)] public float BaseAttackSpeed { get; private set; } = 0.8f;
        [field: SerializeField] public float BaseProjectileSpeed { get; private set; } = 5f;
        [field: SerializeField, Min(Constants.MinBaseShipLimit)] public int BaseShipLimit { get; private set; } = 10;
    }
}





























