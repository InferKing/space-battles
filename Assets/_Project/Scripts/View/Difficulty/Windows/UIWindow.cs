using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.View.Difficulty
{
    public abstract class UIWindow : MonoBehaviour
    {
        public abstract Tween Show();
        public abstract Tween Hide();
    }
}
