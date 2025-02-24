using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Model.GameParameters
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Config/Map")]
    public class MapConfig : ScriptableObject
    {
        [field: SerializeField] public MapType Map { get; private set; }       
        [field: SerializeField] public Vector2 Size { get; private set; }
        [field: SerializeField] public int Stars { get; private set; }
        [field: SerializeField] public float TeamRadius { get; private set; }
        [field: SerializeField] public float DistanceStars { get; private set; }
        [field: SerializeField] public List<MatchMapEdge> MapEdges { get; private set; }

        public int CountEdges(EdgeAmountType amountType)
        {
            return MapEdges.First(item => item.Type == amountType).Amount;
        }
        
        [System.Serializable]
        public class MatchMapEdge
        {
            [field: SerializeField] public EdgeAmountType Type { get; private set; }
            [field: SerializeField] public int Amount { get; private set; }
        }
    }
}