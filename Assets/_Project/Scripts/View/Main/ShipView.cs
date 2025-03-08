using _Project.Scripts.Model;
using UnityEngine;

namespace _Project.Scripts.View.Main
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public void UpdateView(Team team)
        {
            // TODO: создать фабрику, из которой вытаскивать необходимые цвета
            _renderer.color = team switch
            {
                Team.Red => Color.red,
                Team.Green => Color.green,
                Team.Blue => Color.blue,
                Team.Yellow => Color.yellow,
                _ => Color.white
            };
        }
    }
}