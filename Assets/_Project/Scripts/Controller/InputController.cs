using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Controller
{
    public class InputController : ITickable
    {
        public event Action<Vector2> PlayerTouched;

        private bool _touchedPerTick;
        
        public void Tick()
        {
            if (Input.touchCount == 1 && Input.touches[0].phase is TouchPhase.Began)
            {
                var position = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                if (!IsHitUI(position))
                {
                    PlayerTouched?.Invoke(position);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                if (!IsHitUI(position))
                {
                    PlayerTouched?.Invoke(position);
                }
            }
        }

        private bool IsHitUI(Vector3 position)
        {
            var hits = Physics2D.RaycastAll(position, Vector2.zero);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
