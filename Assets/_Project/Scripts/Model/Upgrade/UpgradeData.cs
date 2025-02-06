using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Model.Upgrade
{
    [CreateAssetMenu(fileName = "Upgrade data", menuName = "Config/Upgrade data")]
    public class UpgradeData : ScriptableObject
    {
        [field: SerializeField] public UpgradeType Type { get; private set; }
        [field: SerializeField] public List<float> UpgradeValues { get; private set; }
        [field: SerializeField] public bool HasUnlimitedUpgrade { get; private set; }
        [field: SerializeField] public float UnlimitedUpgradeBy { get; private set; }
    }
}