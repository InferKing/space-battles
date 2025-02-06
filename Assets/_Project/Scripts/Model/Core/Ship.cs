using UnityEngine;
using Zenject;

namespace _Project.Scripts.Model.Core
{
    public class Ship : MonoBehaviour
    {
        public Team Team { get; private set; }

        [Inject]
        private void Construct(Team team)
        {
            Team = team;
            Debug.Log(Team);
        }
        
        public class Factory : PlaceholderFactory<Team, Ship> {}
    }
}