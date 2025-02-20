using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.View.Difficulty
{
    public class UITeam : UIWindow
    {
        private const float Duration = 0.5f;
        
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