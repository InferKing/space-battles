using TMPro;
using UnityEngine;

namespace _Project.Scripts.Model.StarSystem
{
    public class StarView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _toColor;
        [SerializeField] private TMP_Text _text;
        
        public void UpdateView(Team team)
        {
            _toColor.color = team switch
            {
                Team.Red => Color.red,
                Team.Green => Color.green,
                Team.Blue => Color.blue,
                Team.Yellow => Color.yellow,
                _ => Color.white
            };
        }

        public void UpdateHealth(float health)
        {
            _text.text = $"{health:N0}";
            
        }
    }
}