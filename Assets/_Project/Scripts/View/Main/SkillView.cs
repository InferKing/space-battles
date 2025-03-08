using _Project.Scripts.Model.Player;
using _Project.Scripts.Model.Upgrades;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.View.Main
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _applyButton;

        private Player _player;
        private Upgrade _upgrade;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        public void Init(Upgrade upgrade)
        {
            _upgrade = upgrade;
            
            _applyButton.onClick.AddListener(() =>
            {
                _player.ApplyUpgrade(_upgrade);
            });
            
            _player.LevelSystem.FreePoints.Subscribe(change =>
            {
                if (_applyButton.gameObject.activeSelf) return;
                
                _applyButton.gameObject.SetActive(change > 0);
            }).AddTo(this);

            _player.UpgradeManager.Levels.ObserveReplace().Subscribe(change =>
            {
                if (change.Key != _upgrade.Type) return;

                _text.text = change.NewValue.ToString();
            }).AddTo(this);
        }
    }
}