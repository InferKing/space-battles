using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Configs;
using UnityEngine;

namespace _Project.Scripts.Model
{
    [CreateAssetMenu(fileName = "TeamDataManager", menuName = "Config/Team Data Manager")]
    public class TeamsData : ScriptableObject
    {
        [SerializeField] private List<PlayerData> _playerDataList;
        
        public IReadOnlyList<PlayerData> PlayerDataList => _playerDataList;

        public PlayerData GetPlayerData(Team team)
        {
            return _playerDataList.First(data => data.Team == team);
        }
    }
}