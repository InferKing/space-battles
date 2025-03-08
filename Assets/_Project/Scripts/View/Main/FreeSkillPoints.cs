using _Project.Scripts.Model.Player;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Main
{
    public class FreeSkillPoints : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        private void Start()
        {
            _player.LevelSystem.FreePoints.Subscribe(change =>
            {
                _text.text = change.ToString();
            }).AddTo(this);
        }
    }
}