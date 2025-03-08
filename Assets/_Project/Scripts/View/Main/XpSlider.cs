using _Project.Scripts.Model.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.View.Main
{
    public class XpSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private Player _player;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        private void Start()
        {
            _player.LevelSystem.CurrentExp.AsObservable().Subscribe(value =>
            {
                _slider.value = value;
                _slider.minValue = 0;
                _slider.maxValue = _player.LevelSystem.ExpToLevelUp;
            }).AddTo(this);
        }
    }
}