using _Project.Scripts.Model;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.View.Difficulty.Windows
{
    public class UIParameters : UIWindow
    {
        [SerializeField] private SliderText _starCount;
        [SerializeField] private SliderText _teamRadius;
        [SerializeField] private SliderText _edgeCount;
        [SerializeField] private SliderText _areaSize;
        
        private const float Duration = 0.5f;

        private void Start()
        {
            _starCount.Init(Constants.MinStars, Constants.MaxStars, Constants.OptimalStarCount);
            _teamRadius.Init(Constants.MinTeamRadius, Constants.MaxTeamRadius, Constants.OptimalTeamRadius);
            _edgeCount.Init(Constants.MinStars - 1, Constants.MaxStars - 1, Constants.OptimalEdgeCount);
            _areaSize.Init(Constants.MinGenerationArea, Constants.MaxGenerationArea, Constants.OptimalGenerationArea);
        }

        public override Tween Show()
        {
            transform.localScale = Vector3.zero;
            return transform.DOScale(Vector3.one, Duration).SetEase(Ease.OutBack);
        }

        public override Tween Hide()
        {
            transform.localScale = Vector3.one;
            return transform.DOScale(Vector3.zero, Duration).SetEase(Ease.InBack);
        }
    }
}