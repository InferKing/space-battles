using UnityEngine;

namespace _Project.Scripts.View
{
    public class BackgroundStars : MonoBehaviour
    {
        private float _initialSizeCamera;
        
        private void Start()
        {
            if (Camera.main != null) _initialSizeCamera = Camera.main.orthographicSize;
        }

        private void Update()
        {
            transform.localScale = Vector3.one * _initialSizeCamera / 5f;
            transform.localPosition = Camera.main.transform.position * -0.1f;
        }
    }
}