using UnityEngine;

namespace _Project.Scripts.Model
{
    [CreateAssetMenu(fileName = "Difficulty", menuName = "Scriptable Objects/Difficulty")]
    public class Difficulty : ScriptableObject
    {
        // TODO: какие настройки будут влиять на сложность?
        [field: SerializeField]
        public DifficultyType Type { get; private set; }
    }
}
