using System.Collections.Generic;
using _Project.Scripts.Model;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Difficulty
{
    public class PickTeamDescription : MonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private List<TeamAttributeSetter> _teamAttributeSetters;
    
        private TeamsData _teamsData;
    
        [Inject]
        private void Construct(TeamsData teamsData)
        {
            _teamsData = teamsData;
        }

        private void Start()
        {
            // TODO: переделать нахуй. почему switch?????
            for (var i = 0; i < _teamAttributeSetters.Count; i++)
            {
                var playerData = _teamsData.GetPlayerData(_team);

                switch (i)
                {
                    case 0:
                        _teamAttributeSetters[i].SetData(Constants.MinBaseStarHealth, Constants.MaxBaseStarHealth, playerData.BaseStarHealth, 0);
                        break;
                    case 1:
                        _teamAttributeSetters[i].SetData(Constants.MinBaseShipHealth, Constants.MaxBaseShipHealth, playerData.BaseShipHealth, 0.1f);
                        break;
                    case 2:
                        _teamAttributeSetters[i].SetData(Constants.MinBaseDamage, Constants.MaxBaseDamage, playerData.BaseDamage, 0.2f);
                        break;
                    case 3:
                        _teamAttributeSetters[i].SetData(Constants.MinBaseAttackSpeed, Constants.MaxBaseAttackSpeed, playerData.BaseAttackSpeed, 0.3f, false);
                        break;
                    case 4:
                        _teamAttributeSetters[i].SetData(Constants.MinBaseSpeed, Constants.MaxBaseSpeed, playerData.BaseMaxSpeed, 0.4f);
                        break;
                    case 5:
                        _teamAttributeSetters[i].SetData(Constants.MinBaseShipLimit, Constants.MaxBaseShipLimit, playerData.BaseShipLimit, 0.5f);
                        break;
                }
            }
        }
    }
}
